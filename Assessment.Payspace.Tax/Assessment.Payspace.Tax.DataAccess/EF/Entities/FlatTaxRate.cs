using System;

namespace Assessment.Payspace.Tax.DataAccess.EF.Entities
{
    public partial class FlatTaxRate
    {
        public int FlatTaxRateId { get; set; }
        public decimal Percentage { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
