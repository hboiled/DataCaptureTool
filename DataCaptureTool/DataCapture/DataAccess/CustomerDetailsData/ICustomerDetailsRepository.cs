using DataCapture.ViewModels;
using System.Collections.Generic;

namespace DataCapture.DataAccess.CustomerDetailsData
{
    public interface ICustomerDetailsRepository
    {
        CustomerDetailsViewModel GetCustomerById(int id);
        List<CustomerDetailsViewModel> GetCustomerDetails();
        int SaveCustomerDetails(CustomerDetailsViewModel customer);
    }
}