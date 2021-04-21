using BikeRentalService.Models;
using BikeRentalService.Models.Entities;
using BikeRentalService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeRentalService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BicycleRentalDbContext _context;
        private List<SelectListItem> _customerStatus;
        public CustomerRepository(BicycleRentalDbContext context)
        {
            _context = context;

            _customerStatus = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Active", Text = "Active" },
                new SelectListItem { Value = "InActive", Text = "InActive" }
            };
        }

        public CustomerViewModel CreateCustomer()
        {
            return new CustomerViewModel
            {
                CustomerId = Guid.NewGuid(),
                Statuses = _customerStatus
            };
        }

        public async Task<CustomerViewModel> GetCustomer(Guid? id)
        {
            var customer = await _context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == id);

            if(customer != null)
            {
                var customerDisplay = new CustomerViewModel
                {
                    CustomerId = customer.CustomerId,
                    FullName = string.Format("{0}, {1}", customer.LastName, customer.FirstName),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Status = customer.Status,
                    BirthDate = customer.BirthDate,
                    Statuses = _customerStatus
                };

                return customerDisplay;
            }

            return null;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            var customers = await _context.Customers.AsNoTracking().ToListAsync();

            if(customers != null)
            {
                var customersDisplay = new List<CustomerViewModel>();

                foreach (var customer in customers)
                {
                    var customerDisplay = new CustomerViewModel
                    {
                        CustomerId = customer.CustomerId,
                        FullName = string.Format("{0}, {1}", customer.LastName, customer.FirstName),
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Address = customer.Address,
                        Status = customer.Status,
                        BirthDate = customer.BirthDate,
                        Statuses = _customerStatus
                    };

                    customersDisplay.Add(customerDisplay);
                }

                return customersDisplay;
            }

            return null;
        }

        public async Task<bool> SaveCustomer(CustomerViewModel model)
        {
            if (model != null)
            {
                var customer = new Customer
                {
                    CustomerId = model.CustomerId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Status = model.Status,
                    BirthDate = model.BirthDate
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCustomer(CustomerViewModel model)
        {
            if (model != null)
            {
                var customer = new Customer
                {
                    CustomerId = model.CustomerId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Status = model.Status,
                    BirthDate = model.BirthDate
                };

                var current = await _context.Customers.FindAsync(model.CustomerId);

                _context.Entry(current).CurrentValues.SetValues(customer);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCustomer(Guid? id)
        {
            if(id != null)
            {
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
