using Assessment.Payspace.Tax.Dto;
using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;
using Assessment.Payspace.Tax.Interface.Logic;
using Assessment.Payspace.Tax.Logic.Core;
using Assessment.Payspace.Tax.Logic.Core.TaxType;
using System;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Logic
{
    public class TaxLogic : ITaxLogic
    {
        private readonly TaxController<Key, TaxEventHandler> _taxController;
        private readonly ITaxDataAccess _taxDataAccess;
       
        public TaxLogic(ITaxDataAccess taxDataAccess)
        {
            TaxEventHandler taxEventHandler;
            Key key;

            _taxDataAccess = taxDataAccess;

            _taxController = new TaxController<Key, TaxEventHandler>();

            //set up tax handlers
            taxEventHandler = new ProgressiveTaxEvent(_taxDataAccess);
            key = new Key { TaxCode = "7441" };
            _taxController.AddEventHandler(key, taxEventHandler);

            taxEventHandler = new FlatValueTaxEvent(_taxDataAccess);
            key = new Key { TaxCode = "A100" };
            _taxController.AddEventHandler(key, taxEventHandler);

            taxEventHandler = new FlatRateTaxEvent(_taxDataAccess);
            key = new Key { TaxCode = "7000" };
            _taxController.AddEventHandler(key, taxEventHandler);

            taxEventHandler = new ProgressiveTaxEvent(_taxDataAccess);
            key = new Key { TaxCode = "1000" };
            _taxController.AddEventHandler(key, taxEventHandler);
        }

        #region Lookup
        public List<TaxType> GetTaxTypes()
        {
            List<TaxType> taxAssessment = null;
            try
            {
                taxAssessment = _taxDataAccess.GetTaxTypes();
            }
            catch (Exception exception)
            {
                throw;
            }
            return taxAssessment;
        }

        #endregion

        public decimal TaxAssessment(TaxQuery taxQuery)
        {
            TaxAssessment taxAssessment = null;
            TaxType taxType = null;
            decimal result = 0;
            try
            {
                //execute the document channel event
                result = _taxController.ExecuteEvent(taxQuery.PostalCode, taxQuery.Amount);
                //get tax type id
                taxType = _taxDataAccess.GetTaxTypeByPostalCode(taxQuery.PostalCode);

                taxAssessment = new TaxAssessment
                {
                    NettIncome = taxQuery.Amount,
                    IncomeTax = result,
                    TaxTypeId = taxType.TaxTypeId ,
                    DateCreated = DateTime.Now,
                    UserCreated = "System"
                };
                //save assessment calculation
                _taxDataAccess.AddTaxAssessment(taxAssessment);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
