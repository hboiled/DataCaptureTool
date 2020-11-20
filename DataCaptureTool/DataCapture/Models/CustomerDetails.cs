using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.Models
{
    public class CustomerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public PhoneNumber PhoneNumber { get; set; }
        //public DriversLicense DriversLicenseNumber { get; set; }
    }
}
