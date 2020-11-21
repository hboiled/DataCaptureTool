using DataCapture.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataCaptureTests.DataAccess.CustomerDetailsData
{
    public class TestDataCustomerDetails : IEnumerable<object[]>
    {
        private readonly List<object[]> Customers = new List<object[]>
        {
            // each object[] represents a different test
            // objects within the arr represent the test's parameters
            new object[]
            {
                new CustomerDetailsViewModel
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Obo",
                    DateOfBirth = new DateTime(1991, 10, 10),
                    PhoneNumber = 12345678,
                    DriversLicenseNumber = "1298765",
                    StreetAddress = "123 Strett, Subuurb, Ciity, SAW, 1290"
                }
            },
            new object[]
            {
                new CustomerDetailsViewModel
                {
                    Id = 1,
                    FirstName = "ZZ",
                    LastName = "Azx",
                    DateOfBirth = new DateTime(1983, 11, 10),
                    PhoneNumber = 41236789,
                    DriversLicenseNumber = "1298765",
                    StreetAddress = "456 Streett, Suubuurb, Ciitty, JLW, 4230"
                }
            }
        };
        
    public IEnumerator<object[]> GetEnumerator() => Customers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
