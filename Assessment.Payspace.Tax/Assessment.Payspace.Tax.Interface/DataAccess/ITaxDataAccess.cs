using Assessment.Payspace.Tax.Dto.DataAccess;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Interface.DataAccess
{
    public interface ITaxDataAccess
    {
        List<TaxType> GetTaxTypes();

        TaxType GetTaxTypeByPostalCode(string postalCode);

        FlatTaxRate GetFlatTaxRate();

        FlatValueTaxRate GetFlatValueTaxRate();

        List<ProgressiveTaxRate> GetProgressiveTaxRates();

        int AddTaxAssessment(TaxAssessment taxAssessment);
    }
}
