using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;

namespace Assessment.Payspace.Tax.Logic.Core.TaxType
{
    public class FlatRateTaxEvent : TaxEventHandler
    {
        private readonly ITaxDataAccess _taxDataAccess;

        public FlatRateTaxEvent(ITaxDataAccess taxDataAccess)
        {
            _taxDataAccess = taxDataAccess;
        }

        public override decimal TaxCalculator(decimal amount)
        {
            FlatTaxRate flatTaxRate = null;
            decimal result = 0;

            if (amount > 0)
            {
                flatTaxRate = _taxDataAccess.GetFlatTaxRate();

                if(flatTaxRate != null)
                {
                    result = amount * decimal.Parse((flatTaxRate.Percentage / 100).ToString());
                }
            }

            return result;
        }
    }
}
