using System;
using System.Collections.Generic;

namespace Assessment.Payspace.Tax.DataAccess.EF.Entities
{
    public partial class TaxType
    {
        public TaxType()
        {
            TaxAssessment = new HashSet<TaxAssessment>();
        }

        public int TaxTypeId { get; set; }
        public string PostalCode { get; set; }
        public string CalculationType { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserChange { get; set; }
        public DateTime? DateChanged { get; set; }

        public virtual ICollection<TaxAssessment> TaxAssessment { get; set; }
    }
}
