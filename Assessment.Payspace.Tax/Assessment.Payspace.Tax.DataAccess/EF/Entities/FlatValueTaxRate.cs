using System;

namespace Assessment.Payspace.Tax.DataAccess.EF.Entities
{
    public partial class FlatValueTaxRate
    {
        public int FlatValueTaxRateId { get; set; }
        public decimal Percentage { get; set; }
        public decimal Value { get; set; }
        public decimal MaximumAmount { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
