using System;

namespace Assessment.Payspace.Tax.Dto.DataAccess
{
    public class ProgressiveTaxRate
    {
        public int ProgressiveTaxRateId { get; set; }
        public decimal Percentage { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal? MaximumAmount { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
