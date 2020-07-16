using Assessment.Payspace.Tax.Dto;
using Assessment.Payspace.Tax.Dto.DataAccess;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.Interface.Logic
{
    public interface ITaxLogic
    {
        List<TaxType> GetTaxTypes();

        decimal TaxAssessment(TaxQuery taxQuery);
    }
}
