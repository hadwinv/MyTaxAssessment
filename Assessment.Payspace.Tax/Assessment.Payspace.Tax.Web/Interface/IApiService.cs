using Assessment.Payspace.Tax.Web.Data;
using Assessment.Payspace.Tax.Web.Models;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Web.Interface
{
    public interface IApiService
    {
        List<TaxType> GetTaxTypes();

        decimal CalculateTaxAmount(Request request);
    }
}
