using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;

namespace Assessment.Payspace.Tax.Logic.Core.TaxType
{
    public class FlatValueTaxEvent : TaxEventHandler
    {
        private readonly ITaxDataAccess _taxDataAccess;

        public FlatValueTaxEvent(ITaxDataAccess taxDataAccess)
        {
            _taxDataAccess = taxDataAccess;
        }

        public override decimal TaxCalculator(decimal amount)
        {
            FlatValueTaxRate flatValueTaxRate = null;
            decimal result = 0;

            if (amount > 0)
            {
                flatValueTaxRate = _taxDataAccess.GetFlatValueTaxRate();

                if (amount < flatValueTaxRate.MaximumAmount)
                    result = amount * decimal.Parse((flatValueTaxRate.Percentage / 100).ToString());
                else
                    result = flatValueTaxRate.Value;
            }

            return result;
        }
    }
}
