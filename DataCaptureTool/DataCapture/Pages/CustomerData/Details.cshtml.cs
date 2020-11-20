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
    public class DetailsModel : PageModel
    {
        private readonly ICustomerDetailsRepository db;

        public DetailsModel(ICustomerDetailsRepository db)
        {
            this.db = db;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            CustomerDetails = db.GetCustomerById((int)id);

            if (CustomerDetails == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public CustomerDetailsViewModel CustomerDetails { get; set; }

        public void OnGetCustomerDetails(CustomerDetailsViewModel customerDetails)
        {
            CustomerDetails = customerDetails;
        }
    }
}
