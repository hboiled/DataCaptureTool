using DataCapture.DataAccess.CustomerDetailsData;
using DataCapture.DataAccess.Sqlite;
using DataCapture.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DataCaptureTests.DataAccess.CustomerDetailsData
{
    public class CustomerDetailsDAOTests
    {
        private string conStr = "conStrMoq";
        private Mock<ISqliteDataAccess> db;
        private CustomerDetailsDAO dao;

        public CustomerDetailsDAOTests()
        {
            db = new Mock<ISqliteDataAccess>();
            dao = new CustomerDetailsDAO(conStr, db.Object);
        }

        [Fact]
        public void GetCustomerDetails_ReturnsListOfCustomers()
        {
            // arrange
            var customers = GetListOfSampleCustomers();
            string sqlStatement = SelectAllCustomersSqlStatement();

            db.Setup(x => x.LoadData<CustomerDetailsViewModel, dynamic>(
                sqlStatement, It.IsAny<object>(), conStr))
              .Returns(customers);

            // result
            var expected = GetListOfSampleCustomers();
            var actual = dao.GetCustomerDetails();

            // assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetCustomerById_ReturnsValidCustomerDetails(int id)
        {
            List<CustomerDetailsViewModel> customers = GetListOfSampleCustomers()
                .Where(c => c.Id == id)
                .ToList();

            string sql = SelectCustomerByIdSqlStatement();

            db.Setup(x => x.LoadData<CustomerDetailsViewModel, dynamic>
                (sql, It.IsAny<object>(), conStr))
                .Returns(customers);

            // result
            var expected = customers.FirstOrDefault();
            var actual = dao.GetCustomerById(id);

            // assert
            Assert.True(actual != null);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestDataCustomerDetails))]
        public void SaveCustomerDetails_HandlesSubmissionsCorrectly(CustomerDetailsViewModel customer)
        {
            string sqlPerson = CreateCustomerSql()["SaveCustomerRetrieveId"];
            string sqlPhoneNumbers = CreateCustomerSql()["SavePhoneNumbers"];
            string sqlDriversLicense = CreateCustomerSql()["SaveDriversLicense"];
            string sqlAddresses = CreateCustomerSql()["SaveAddress"];

            List<int> Ids = new List<int> { customer.Id };

            db.Setup(x => x.LoadData<int, dynamic>
                (sqlPerson, It.IsAny<object>(), conStr))
                .Returns(Ids);

            dao.SaveCustomerDetails(customer);

            db.Verify(x => x.LoadData<int, dynamic>(sqlPerson, It.IsAny<object>(), conStr), Times.Exactly(1));
            db.Verify(x => x.ExecuteStatement<dynamic>(sqlPhoneNumbers, It.IsAny<object>(), conStr), Times.Exactly(1));
            db.Verify(x => x.ExecuteStatement<dynamic>(sqlDriversLicense, It.IsAny<object>(), conStr), Times.Exactly(1));
            db.Verify(x => x.ExecuteStatement<dynamic>(sqlAddresses, It.IsAny<object>(), conStr), Times.Exactly(1));
        }

        private string SelectAllCustomersSqlStatement()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Customers.Id, Customers.FirstName, Customers.LastName, Customers.DateOfBirth, Addresses.StreetAddress, PhoneNumbers.PhoneNumber, DriversLicenses.DriversLicenseNumber FROM Customers ");
            sql.Append("INNER JOIN Addresses on Addresses.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN PhoneNumbers on PhoneNumbers.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN DriversLicenses on DriversLicenses.CustomerId = Customers.Id ");

            return sql.ToString();            
        }

        private string SelectCustomerByIdSqlStatement()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Customers.Id, Customers.FirstName, Customers.LastName, Customers.DateOfBirth, Addresses.StreetAddress, PhoneNumbers.PhoneNumber, DriversLicenses.DriversLicenseNumber FROM Customers ");
            sql.Append("INNER JOIN Addresses on Addresses.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN PhoneNumbers on PhoneNumbers.CustomerId = Customers.Id ");
            sql.Append("INNER JOIN DriversLicenses on DriversLicenses.CustomerId = Customers.Id ");
            sql.Append("WHERE Customers.Id = @Id ");

            return sql.ToString();
        }

        private Dictionary<string, string> CreateCustomerSql()
        {
            return new Dictionary<string, string>
            {                 
                {
                    "SaveCustomerRetrieveId",
                    "INSERT INTO Customers (FirstName, LastName, DateOfBirth) " +
                    "values (@FirstName, @LastName, @DateOfBirth);"
                   + "select last_insert_rowid();"
                },
                {
                    "SavePhoneNumbers",
                    "INSERT INTO PhoneNumbers (CustomerId, PhoneNumber) " +
                    "values (@CustomerId, @PhoneNumber);"
                },
                {
                    "SaveDriversLicense",
                    "INSERT INTO DriversLicenses (CustomerId, DriversLicenseNumber) " +
                    "values (@CustomerId, @DriversLicenseNumber);"
                },
                {
                    "SaveAddress",
                    "INSERT INTO Addresses (CustomerId, StreetAddress) " +
                    "values (@CustomerId, @StreetAddress);"
                }
                };
        }

        private List<CustomerDetailsViewModel> GetListOfSampleCustomers()
        {
            return new List<CustomerDetailsViewModel>
            {
                new CustomerDetailsViewModel
                {
                    Id = 1,
                    FirstName = "Qwerty",
                    LastName = "Asdfg",
                    DateOfBirth = new DateTime(1999, 12, 10),
                    PhoneNumber = 12345678,
                    StreetAddress = "5 Aplace, MySuburb, TheCity, QWK, 1239",
                    DriversLicenseNumber = "4231678",
                },
                new CustomerDetailsViewModel
                {
                    Id = 2,
                    FirstName = "Zwerty",
                    LastName = "Asdfgge",
                    DateOfBirth = new DateTime(1959, 12, 10),
                    PhoneNumber = 12345678,
                    StreetAddress = "5 Aplface, MySrburb, ThevCity, PWK, 2239",
                    DriversLicenseNumber = "4231676",
                },
                new CustomerDetailsViewModel
                {
                    Id = 3,
                    FirstName = "Qgwety",
                    LastName = "Aslekg",
                    DateOfBirth = new DateTime(1992, 12, 10),
                    PhoneNumber = 12345671,
                    StreetAddress = "5 Aplzce, MyVuburb, TheAity, QBK, 4239",
                    DriversLicenseNumber = "4231618",
                }
            };
        }
    }
}
