using DataCapture.DataAccess.Sqlite;
using DataCapture.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCapture.DataAccess.CustomerDetailsData
{
    /// <summary>
    /// Simplified repository.
    /// 
    /// Makes use of a model with all data consolidated into one, with the same concept applied to the table.
    /// Read/write operations follow a simpler pattern where all data flows through one model.
    /// 
    /// Much easier to implement and use but less scalable in the long run.
    /// </summary>
    public class CustomerDetailsDataSimple : ICustomerDetailsDataSimple
    {
        private readonly string connectionString;
        private readonly ISqliteDataAccess db;

        public CustomerDetailsDataSimple(string connectionString, ISqliteDataAccess db)
        {
            this.connectionString = connectionString;
            this.db = db;
        }

        public List<CustomerDetailsViewModel> GetCustomerDetails()
        {
            string sqlStatement = "SELECT * FROM CustomersSimple";

            List<CustomerDetailsViewModel> customers = db.LoadData<CustomerDetailsViewModel, dynamic>(
                sqlStatement,
                new { },
                connectionString);

            return customers;
        }

        public CustomerDetailsViewModel GetCustomerById(int id)
        {
            string sql = "SELECT * FROM CustomersSimple WHERE Id = @Id";

            var customer = db.LoadData<CustomerDetailsViewModel, dynamic>(
                sql,
                new { Id = id },
                connectionString)
                .FirstOrDefault();

            return customer;
        }

        public int SaveCustomerDetails(CustomerDetailsViewModel customer)
        {
            string sqlStatement = "INSERT INTO CustomersSimple (FirstName, LastName, DateOfBirth, PhoneNumber, DriversLicenseNumber, StreetAddress) " +
                "values (@FirstName, @LastName, @DateOfBirth, @PhoneNumber, @DriversLicenseNumber, @StreetAddress);"
                + "select last_insert_rowid();";

            int customerId = db.LoadData<int, dynamic>(
                sqlStatement,
                new
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth,
                    PhoneNumber = customer.PhoneNumber,
                    DriversLicenseNumber = customer.DriversLicenseNumber,
                    StreetAddress = customer.StreetAddress
                },
                connectionString)
                .FirstOrDefault();

            return customerId;
        }
    }
}
