using BikeRentalService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeRentalService.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerViewModel>> GetCustomers();

        Task<CustomerViewModel> GetCustomer(Guid? id);

        Task<bool> UpdateCustomer(CustomerViewModel model);

        CustomerViewModel CreateCustomer();

        Task<bool> SaveCustomer(CustomerViewModel model);

        Task<bool> DeleteCustomer(Guid? id);
    }
}