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
        private readonly ICustomerDetailsDAO db;

        public CustomersModel(ICustomerDetailsDAO db)
        {
            this.db = db;
        }

        [BindProperty]
        public string ErrorMsg { get; set; }

        [BindProperty]
        public List<CustomerDetailsViewModel> Customers_A_To_H { get; set; } = new List<CustomerDetailsViewModel>();
        [BindProperty]
        public List<CustomerDetailsViewModel> Customers_I_To_P { get; set; } = new List<CustomerDetailsViewModel>();
        [BindProperty]
        public List<CustomerDetailsViewModel> Customers_Q_To_Z { get; set; } = new List<CustomerDetailsViewModel>();

        public void OnGet()
        {
            List<CustomerDetailsViewModel> customerDetails = db.GetCustomerDetails();
            
            if (customerDetails.Count > 0)
            {
                Customers_A_To_H = customerDetails
                    .Where(c => c.LastName.ToLower()[0].CompareTo('h') <= 0)
                    .OrderBy(c => c.LastName)
                    .ToList();

                Customers_I_To_P = customerDetails
                    .Where(c => c.LastName.ToLower()[0].CompareTo('p') <= 0 && c.LastName.ToLower()[0].CompareTo('i') >= 0)
                    .OrderBy(c => c.LastName)
                    .ToList();

                Customers_Q_To_Z = customerDetails
                    .Where(c => c.LastName.ToLower()[0].CompareTo('q') >= 0)
                    .OrderBy(c => c.LastName)
                    .ToList();

            }
            else
            {                
                ErrorMsg = "There are currently no entries.";
            }
        }


    }
}
