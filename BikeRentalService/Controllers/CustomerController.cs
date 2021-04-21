using BikeRentalService.Models.Entities;
using BikeRentalService.Models.ViewModels;
using BikeRentalService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BikeRentalService.Controllers
{
    [Authorize(Roles = "Owner")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepo.GetCustomers();

            if (customers == null)
                return NoContent();

            return View(customers);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var customer = await _customerRepo.GetCustomer(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind(include: new string[] { "CustomerId", "FirstName", "LastName", "Address", "BirthDate", "Status" })] CustomerViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _customerRepo.UpdateCustomer(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }

        public IActionResult Create()
        {
            var customer = _customerRepo.CreateCustomer();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: new string[] { "CustomerId", "FirstName", "LastName", "Address", "BirthDate", "Status" })] CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerRepo.SaveCustomer(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }

        public async Task<IActionResult> Detail(Guid? id)
        {
            var customer = await _customerRepo.GetCustomer(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            var customer = await _customerRepo.GetCustomer(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid? CustomerId)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerRepo.DeleteCustomer(CustomerId);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }
    }
}
