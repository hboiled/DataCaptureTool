using DataCapture.DataAccess.Sqlite;
using DataCapture.Models;
using DataCapture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.DataAccess.CustomerDetailsData
{
    public class CustomerDetailsData
    {
        private readonly string connectionString;
        private readonly ISqliteDataAccess db;

        public CustomerDetailsData(string connectionString, ISqliteDataAccess db)
        {
            this.connectionString = connectionString;
            this.db = db;
        }

        public List<CustomerDetailsViewModel> GetCustomerDetails()
        {
            string sqlStatement = "SELECT * FROM Customers";

            List<CustomerDetails> customers = db.LoadData<CustomerDetails, dynamic>(
                sqlStatement,
                new { },
                connectionString);

            List<CustomerDetailsViewModel> customerDetails = new List<CustomerDetailsViewModel>();

            foreach (var customer in customers)
            {
                CustomerDetailsViewModel c = MapToCustomerDetailsVM(customer);
                customerDetails.Add(c);
            }

            return customerDetails;
        }

        // calling method should null check
        public CustomerDetailsViewModel GetCustomerById(int id)
        {
            string sqlStatement = "SELECT * FROM Customers WHERE Id = @Id";

            CustomerDetails customer = db.LoadData<CustomerDetails, dynamic>(
                sqlStatement,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return MapToCustomerDetailsVM(customer);
        }

        private CustomerDetailsViewModel MapToCustomerDetailsVM(CustomerDetails customer)
        {
            int customerId = customer.Id;

            var phoneNumber = GetPhoneNumberById(customerId);
            var driversLicense = GetDriversLicenseById(customerId);

            CustomerDetailsViewModel customerDetails = new CustomerDetailsViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = phoneNumber.Number,
                DriversLicenseNumber = driversLicense.DriversLicenseNumber
            };

            return customerDetails;
        }

        private PhoneNumber GetPhoneNumberById(int id)
        {
            string sqlStatement = "SELECT * FROM PhoneNumbers WHERE Id = @Id";

            var phoneNumber = db.LoadData<PhoneNumber, dynamic>(
                sqlStatement,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return phoneNumber;
        }

        private DriversLicense GetDriversLicenseById(int id)
        {
            string sqlStatement = "SELECT * FROM DriversLicenses WHERE Id = @Id";

            var driversLicense = db.LoadData<DriversLicense, dynamic>(
                sqlStatement,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return driversLicense;
        }

        private Address GetAddressById(int id)
        {
            string sqlStatement = "SELECT * FROM Addresses WHERE Id = @Id";

            var address = db.LoadData<Address, dynamic>(
                sqlStatement,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return address;
        }

        public int SaveCustomerDetails(CustomerDetailsViewModel customer)
        {
            // can be improved using transactions

            string sqlSaveCustomerRetrieveId = "INSERT INTO Customers (FirstName, LastName, DateOfBirth) " +
                "values (@FirstName, @LastName, @DateOfBirth);"
                + "select last_insert_rowid();";

            int customerId = db.LoadData<int, dynamic>(
                sqlSaveCustomerRetrieveId,
                new
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth
                },
                connectionString)
                .FirstOrDefault();

            // save phone numbers
            string sqlSavePhoneNumbers = "INSERT INTO PhoneNumbers (CustomerId, Number) " +
                "values (@CustomerId, @Number);";

            db.ExecuteStatement<dynamic>(
                sqlSavePhoneNumbers,
                new { CustomerId = customerId, Number = customer.PhoneNumber },
                connectionString);

            // save drivers licenses
            string sqlSaveDriversLicense = "INSERT INTO DriversLicenses (CustomerId, DriversLicenseNumber) " +
                "values (@CustomerId, @DriversLicenseNumber);";

            db.ExecuteStatement<dynamic>(
                sqlSaveDriversLicense,
                new { CustomerId = customerId, DriversLicenseNumber = customer.DriversLicenseNumber },
                connectionString);
            
            // save address
            string sqlSaveAddress = "INSERT INTO Addresses (CustomerId, StreetAddress) " +
                "values (@CustomerId, @StreetAddress);";

            db.ExecuteStatement<dynamic>(
                sqlSaveDriversLicense,
                new { CustomerId = customerId, StreetAddress = customer.StreetAddress },
                connectionString);

            return customerId;
        }


    }
}
