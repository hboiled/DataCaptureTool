using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCapture.DataAccess.CustomerDetailsData;
using DataCapture.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataCapture.Pages.CustomerData
{
    public class SubmitModel : PageModel
    {
        private readonly ICustomerDetailsDAO db;

        public SubmitModel(ICustomerDetailsDAO db)
        {
            this.db = db;
        }

        [BindProperty]
        public CustomerDetailsViewModel CustomerDetails { get; set; }
        [BindProperty]
        public string ErrorSaving { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {                
                int customerId = db.SaveCustomerDetails(CustomerDetails);
                return RedirectToPage("Details", new { id = customerId });
            }
            catch (Exception ex)
            {
                // error messages kept vague for production
                ErrorSaving = "An error was encountered. Please log this error and send it to the appropriate personnel for further investigation.";
                return Page();
            }
        }
    }
}
