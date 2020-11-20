using DataCapture.DataAccess.CustomerDetailsData;
using DataCapture.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerDetailsRepository db;

        public IndexModel(ILogger<IndexModel> logger, ICustomerDetailsRepository db)
        {
            _logger = logger;
            this.db = db;
        }

        public void OnGet()
        {
            var c = new CustomerDetailsViewModel
            {
                FirstName = "as",
                LastName = "gesg",
                DateOfBirth = new DateTime(2000, 10, 10),
                DriversLicenseNumber = "DL12093",
                PhoneNumber = 89423029
            };

            //db.SaveCustomerDetails(c);

            var r = db.GetCustomerDetails();

            Console.WriteLine();
        }
    }
}
