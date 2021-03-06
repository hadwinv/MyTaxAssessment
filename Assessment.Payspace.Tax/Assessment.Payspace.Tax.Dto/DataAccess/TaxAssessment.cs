﻿using System;

namespace Assessment.Payspace.Tax.Dto.DataAccess
{
    public class TaxAssessment
    {
        public int TaxAssessmentId { get; set; }
        public decimal NettIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public int TaxTypeId { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserChanged { get; set; }
        public DateTime? DateChanged { get; set; }

    }
}
