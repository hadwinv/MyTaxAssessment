using System;

namespace Assessment.Payspace.Tax.Dto.DataAccess
{
    public class TaxType
    {
        public int TaxTypeId { get; set; }
        public string PostalCode { get; set; }
        public string CalculationType { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserChange { get; set; }
        public DateTime? DateChanged { get; set; }
    }
}
