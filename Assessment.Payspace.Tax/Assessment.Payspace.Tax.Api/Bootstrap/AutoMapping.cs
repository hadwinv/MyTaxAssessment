using Assessment.Payspace.Tax.Dto.DataAccess;
using AutoMapper;

namespace Assessment.Payspace.Tax.Api.Bootstrap
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // means you want to map from model to dto
            CreateMap<DataAccess.EF.Entities.FlatTaxRate, FlatTaxRate>();
            CreateMap<DataAccess.EF.Entities.FlatValueTaxRate, FlatValueTaxRate>();
            CreateMap<DataAccess.EF.Entities.ProgressiveTaxRate, ProgressiveTaxRate>();
            CreateMap<DataAccess.EF.Entities.TaxType, TaxType>();
            //two-way mapping
            CreateMap<DataAccess.EF.Entities.TaxAssessment, TaxAssessment>().ReverseMap();
        }
    }
}
