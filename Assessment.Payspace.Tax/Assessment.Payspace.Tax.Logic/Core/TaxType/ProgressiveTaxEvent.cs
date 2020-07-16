using Assessment.Payspace.Tax.Dto.DataAccess;
using Assessment.Payspace.Tax.Interface.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.Payspace.Tax.Logic.Core.TaxType
{
    public class ProgressiveTaxEvent : TaxEventHandler
    {
        private readonly ITaxDataAccess _taxDataAccess;

        public ProgressiveTaxEvent(ITaxDataAccess taxDataAccess)
        {
            _taxDataAccess = taxDataAccess;
        }

        public override decimal TaxCalculator(decimal amount)
        {
            List<ProgressiveTaxRate> progressiveTaxRates = null;
            decimal result = 0;
            decimal runningTotal = 0;

            if (amount > 0)
            {
                progressiveTaxRates = _taxDataAccess.GetProgressiveTaxRates();

                runningTotal = amount;

                foreach (ProgressiveTaxRate taxRate in progressiveTaxRates.OrderBy(x => x.MinimumAmount))
                {
                    
                    //spelling error
                    if (taxRate.MaximumAmount.HasValue)
                    {
                        var taxAmount = taxRate.MaximumAmount.Value - taxRate.MinimumAmount;

                        if(taxAmount <= runningTotal)
                        {
                            result += taxAmount * (taxRate.Percentage / 100);

                            runningTotal -= taxAmount;
                        }
                        else
                        {
                            result += runningTotal * (taxRate.Percentage / 100);

                            runningTotal -= runningTotal;
                        }
                    }
                    else
                    {
                        result += runningTotal * (taxRate.Percentage / 100);

                        runningTotal -= runningTotal;
                    }

                    if (runningTotal == 0)
                        break;
                }
            }

            return result;
        }
    }
}
