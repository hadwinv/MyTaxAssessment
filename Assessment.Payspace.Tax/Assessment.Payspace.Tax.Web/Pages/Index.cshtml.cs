using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using Assessment.Payspace.Tax.Web.Data;
using Assessment.Payspace.Tax.Web.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assessment.Payspace.Tax.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> TaxTypes { get; set; }

        [Display(Name = "Tax Type")]
        [Required]
        [BindProperty]
        public string SelectedTaxType { get; set; }

        [Display(Name = "Amount")]
        [Required]
        [BindProperty]
        public string Amount { get; set; }

        [BindProperty]
        public string IncomeTax { get; set; }

        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet()
        {
            try
            {
                //load tax types
                TaxTypes = _apiService.GetTaxTypes().Select(x => new SelectListItem
                {
                    Value = x.PostalCode,
                    Text = x.PostalCode + " - " + x.CalculationType
                });
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        public ActionResult OnPost()
        {
            Request request = null;
            decimal result = 0;
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                //formulate request
                request = new Request
                {
                    Amount = decimal.Parse(Amount),
                    PostalCode = SelectedTaxType,
                };
                //call to do assessment
                result = _apiService.CalculateTaxAmount(request);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return RedirectToPage("Summary", new  { result = result });
        }
    }
}
