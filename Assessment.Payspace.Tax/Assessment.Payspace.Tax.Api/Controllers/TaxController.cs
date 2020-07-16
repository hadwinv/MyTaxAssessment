using System;
using System.Collections.Generic;
using Assessment.Payspace.Tax.Dto;
using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Payspace.Tax.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxLogic _taxLogic;

        public TaxController(ITaxLogic taxLogic)
        {
            _taxLogic = taxLogic;
        }

        [HttpGet, Route("tax-types")]
        public List<TaxType> GetTaxTypes()
        {
            try
            {
                return _taxLogic.GetTaxTypes();
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        [HttpPost]
        public decimal AssessmentTaxes(TaxQuery taxQuery)
        {
            try
            {
                return _taxLogic.TaxAssessment(taxQuery);
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        
    }
}
