using DataCapture.DataAccess.Sqlite;
using DataCapture.Models;
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

        public List<CustomerDetails> GetCustomerDetails()
        {
            string sqlStatement = "SELECT * FROM Customers";

            List<CustomerDetails> customers = db.LoadData<CustomerDetails, dynamic>(
                sqlStatement,
                new { },
                connectionString);

            return customers;
        }

        // calling method should null check
        public CustomerDetails GetCustomerById(int id)
        {
            string sql = "SELECT * FROM Customers WHERE Id = @Id";

            var customer = db.LoadData<CustomerDetails, dynamic>(
                sql,
                new { Id = id },
                connectionString)
                .FirstOrDefault();
            
            return customer;
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


    }
}
