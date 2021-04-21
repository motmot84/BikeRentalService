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
    [Authorize]
    public class BikeBookingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly UserManager<LoginAccount> _user;

        public BikeBookingController(IBookingRepository bookingRepo, UserManager<LoginAccount> user)
        {
            _bookingRepo = bookingRepo;
            _user = user;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingRepo.GetBookings();

            if (bookings == null)
                return NoContent();

            return View(bookings);
        }
        
        public async Task<IActionResult> Detail(Guid? id)
        {
            var booking = await _bookingRepo.GetBooking(id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var booking = await _bookingRepo.GetBooking(id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind(include: new string[] { "RentalId", "SelectedCustomerId", "SelectedBikeId", "Status", "RentedDate", "ReturnedDate" })] BikeBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _user.GetUserAsync(User);

                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = currentUser.Email;

                var response = await _bookingRepo.UpdateBooking(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }

        public IActionResult Create()
        {
            var booking = _bookingRepo.CreateBooking();

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: new string[] { "RentalId", "SelectedCustomerId", "SelectedBikeId", "Status", "RentedDate", "ReturnedDate" })] BikeBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookingRepo.SaveBooking(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }
    }
}
