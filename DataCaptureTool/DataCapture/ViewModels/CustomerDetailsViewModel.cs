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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 30 characters long")]
        public string FirstName { get; set; }
        
        [Required]
        [DisplayName("Last Name:")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 30 characters long")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth:")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Phone Number:")]
        [RegularExpression(@"(?<!\d)(\d{8}|\d{10})(?!\d)",
            ErrorMessage = "Phone number must be either a mobile or landline without area/country code")]
        public int PhoneNumber { get; set; }
        
        [Required]
        [DisplayName("Drivers License Number:")]
        [RegularExpression("[0-9]{7}", 
            ErrorMessage = "Drivers License number must be only 7 digits")]
        public string DriversLicenseNumber { get; set; }

        [Required]
        [DisplayName("Street Address:")]
        [RegularExpression(@"[0-9]+[a-zA-Z\s]?\s([a-zA-Z\s]+[,]{1}[\s]{1}){3}[A-Z]+[,]{1}[\s]{1}[0-9]{4}", 
            ErrorMessage = "Address must match the following pattern: 123(Optional unit number) StreetName, MySuburb, ThisCity, WA(State acronym), 5912 (4 digit post code)")]
        public string StreetAddress { get; set; }

        public string FullName 
        {
            get 
            {
                return $"{FirstName} {LastName}";
            }
        }

        [DisplayName("Age:")]
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;

                if (today.Month < DateOfBirth.Month ||
                   ((today.Month == DateOfBirth.Month) && (today.Day < DateOfBirth.Day)))
                {
                    age--;
                }

                return age;
            }
        }
    }
}
