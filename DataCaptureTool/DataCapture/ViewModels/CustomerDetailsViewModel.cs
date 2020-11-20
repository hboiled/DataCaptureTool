using DataCapture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name:")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name:")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth:")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Phone Number:")]
        public int PhoneNumber { get; set; }
        
        [Required]
        [DisplayName("Drivers License Number:")]
        public string DriversLicenseNumber { get; set; }

        [Required]
        [DisplayName("Street Address:")]
        public string StreetAddress { get; set; }

        public string FullName 
        {
            get 
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
