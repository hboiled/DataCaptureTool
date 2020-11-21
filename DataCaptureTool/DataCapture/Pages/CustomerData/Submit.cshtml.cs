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

        public void OnGet()
        {
        }

        public IActionResult OnPost()
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
