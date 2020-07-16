using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assessment.Payspace.Tax.Web.Pages
{
    public class SummaryModel : PageModel
    {
        [Display(Name = "Tax Amount : ")]
        public string TaxAmount { get; set; }

        public void OnGet(string result)
        {
            TaxAmount = result;
        }
    }
}