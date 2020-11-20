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
        private readonly ICustomerDetailsDataSimple db;

        public SubmitModel(ICustomerDetailsDataSimple db)
        {
            this.db = db;
        }

        [BindProperty]
        public CustomerDetailsViewModel CustomerDetails { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int customerId = db.SaveCustomerDetails(CustomerDetails);

            return RedirectToPage("Details", new { id = customerId } );
        }
    }
}