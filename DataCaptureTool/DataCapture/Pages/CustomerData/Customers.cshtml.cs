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
    public class CustomersModel : PageModel
    {
        private readonly ICustomerDetailsDataSimple db;

        public CustomersModel(ICustomerDetailsDataSimple db)
        {
            this.db = db;
        }

        [BindProperty]
        public List<CustomerDetailsViewModel> CustomerDetails { get; set; }

        public void OnGet()
        {
            List<CustomerDetailsViewModel> customerDetails = db.GetCustomerDetails();

            if (customerDetails.Count > 0)
            {
                CustomerDetails = customerDetails;
            }
        }


    }
}
