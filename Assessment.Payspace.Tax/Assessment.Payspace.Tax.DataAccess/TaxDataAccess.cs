using Assessment.Payspace.Tax.DataAccess.EF;
using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.Payspace.Tax.DataAccess
{
    public class TaxDataAccess : ITaxDataAccess
    {
        private readonly IMapper _mapper;
        private TaxAdministrationContext _context;

        public TaxAdministrationContext ContextEngine
        {
            get
            {
                if (_context == null)
                    _context = new TaxAdministrationContext();

                return _context;
            }
        }

        public TaxDataAccess()
        {
        }

        public TaxDataAccess(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TaxDataAccess(TaxAdministrationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<TaxType> GetTaxTypes()
        {
            List<TaxType> taxTypes = null;

            var model = ContextEngine.TaxType.ToListReadUncommitted();

            taxTypes = _mapper.Map<List<TaxType>>(model);

            return taxTypes;
        }

        public TaxType GetTaxTypeByPostalCode(string postalCode)
        {
            TaxType taxType = null;

            var model = ContextEngine.TaxType.Where(x => x.PostalCode == postalCode).ReadUncommitted();

            taxType = _mapper.Map<TaxType>(model);

            return taxType;
        }

        public FlatTaxRate GetFlatTaxRate()
        {
            FlatTaxRate flatTaxRate = null;
            
            var model = ContextEngine.FlatTaxRate.ReadUncommitted();

            flatTaxRate = _mapper.Map<FlatTaxRate>(model);

            return flatTaxRate;
        }

        public FlatValueTaxRate GetFlatValueTaxRate()
        {
            FlatValueTaxRate flatValueTaxRate = null;

            var model = ContextEngine.FlatValueTaxRate.ReadUncommitted();

            flatValueTaxRate = _mapper.Map<FlatValueTaxRate>(model);

            return flatValueTaxRate;
        }

        public List<ProgressiveTaxRate> GetProgressiveTaxRates()
        {
            List<ProgressiveTaxRate> progressiveTaxRates = null;

            var model = ContextEngine.ProgressiveTaxRate.ToListReadUncommitted();

            progressiveTaxRates = _mapper.Map<List<ProgressiveTaxRate>>(model);

            return progressiveTaxRates;
        }

        public int AddTaxAssessment(TaxAssessment taxAssessment)
        {
            EF.Entities.TaxAssessment model = _mapper.Map<TaxAssessment, EF.Entities.TaxAssessment>(taxAssessment);

            ContextEngine.TaxAssessment.Add(model);
            ContextEngine.SaveChanges();

            return model.TaxAssessmentId;
        }

    }
}
