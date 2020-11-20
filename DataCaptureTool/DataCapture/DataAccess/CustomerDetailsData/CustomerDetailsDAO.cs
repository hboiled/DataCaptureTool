using DataCapture.DataAccess.Sqlite;
using DataCapture.Models;
using DataCapture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCapture.DataAccess.CustomerDetailsData
{
    public class CustomerDetailsDAO : ICustomerDetailsDAO
    {
        private readonly string connectionString;
        private readonly ISqliteDataAccess db;

        public CustomerDetailsDAO(string connectionString, ISqliteDataAccess db)
        {
            this.connectionString = connectionString;
            this.db = db;
        }

        public List<CustomerDetailsViewModel> GetCustomerDetails()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Customers.Id, Customers.FirstName, Customers.LastName, Customers.DateOfBirth, Addresses.StreetAddress, PhoneNumbers.Number, DriversLicenses.DriversLicenseNumber FROM Customers ");
            sql.Append("INNER JOIN Addresses on Addresses.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN PhoneNumbers on PhoneNumbers.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN DriversLicenses on DriversLicenses.CustomerId = Customers.Id ");

            string sqlStatement = sql.ToString();

            List<CustomerDetailsViewModel> customers = db.LoadData<CustomerDetailsViewModel, dynamic>(
                sqlStatement,
                new { },
                connectionString);

            return customers;
        }

        // calling method should null check
        public CustomerDetailsViewModel GetCustomerById(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Customers.Id, Customers.FirstName, Customers.LastName, Customers.DateOfBirth, Addresses.StreetAddress, PhoneNumbers.Number, DriversLicenses.DriversLicenseNumber FROM Customers ");
            sql.Append("INNER JOIN Addresses on Addresses.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN PhoneNumbers on PhoneNumbers.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN DriversLicenses on DriversLicenses.CustomerId = Customers.Id ");
            sql.Append("WHERE Customers.Id = @Id ");

            string sqlStatement = sql.ToString();

            CustomerDetailsViewModel customer = db.LoadData<CustomerDetailsViewModel, dynamic>(
                sqlStatement,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return customer;
        }

        //private CustomerDetailsViewModel MapToCustomerDetailsVM(CustomerDetails customer)
        //{
        //    int customerId = customer.Id;

        //    var phoneNumber = GetPhoneNumberById(customerId);
        //    var driversLicense = GetDriversLicenseById(customerId);

        //    CustomerDetailsViewModel customerDetails = new CustomerDetailsViewModel
        //    {
        //        FirstName = customer.FirstName,
        //        LastName = customer.LastName,
        //        DateOfBirth = customer.DateOfBirth,
        //        PhoneNumber = phoneNumber.Number,
        //        DriversLicenseNumber = driversLicense.DriversLicenseNumber
        //    };

        //    return customerDetails;
        //}

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
                sqlSaveAddress,
                new { CustomerId = customerId, StreetAddress = customer.StreetAddress },
                connectionString);

            return customerId;
        }


    }
}
