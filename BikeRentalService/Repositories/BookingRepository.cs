using BikeRentalService.Models;
using BikeRentalService.Models.Entities;
using BikeRentalService.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRentalService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BicycleRentalDbContext _context;
        private List<SelectListItem> _bikes;
        private List<SelectListItem> _customers;

        public BookingRepository(BicycleRentalDbContext context)
        {
            _context = context;

            _bikes = _context.BicycleInventories.AsNoTracking()
                .Where(x => x.Status == "Available")
                .Select(x => new SelectListItem
                {
                    Value = x.BikeId.ToString(),
                    Text = x.ModelNo
                }).ToList();

            _customers = _context.Customers.AsNoTracking()
                .Where(x => x.Status == "Active")
                .Select(x => new SelectListItem
                {
                    Value = x.CustomerId.ToString(),
                    Text = string.Format("{0}, {1}", x.LastName, x.FirstName)
                }).ToList();
        }

        public BikeBookingViewModel CreateBooking()
        {
            return new BikeBookingViewModel
            {
                RentalId = Guid.NewGuid(),
                Customers = _customers,
                Bikes = _bikes
            };
        }

        public async Task<BikeBookingViewModel> GetBooking(Guid? id)
        {
            var booking = await _context.BicycleRentals.AsNoTracking()
                .Include(c => c.Customer)
                .Include(b => b.BicycleInventory)
                .FirstOrDefaultAsync(x => x.RentalId == id);

            if (booking != null)
            {
                var currDate = booking.ReturnedDate.HasValue ? booking.ReturnedDate : DateTime.Now;
                var status = booking.ReturnedDate.HasValue ? "Returned" : "Rented";

                var bookingDisplay = new BikeBookingViewModel
                {
                    RentalId = booking.RentalId,
                    RentedDate = booking.RentedDate,
                    ReturnedDate = booking.ReturnedDate,
                    Status = status,
                    SelectedBikeId = booking.BicycleInventory.BikeId,
                    SelectedBikeModel = booking.BicycleInventory.ModelNo,
                    BicycleInventory = booking.BicycleInventory,
                    SelectedCustomerId = booking.Customer.CustomerId,
                    CustomerFullName = string.Format("{0}, {1}", booking.Customer.LastName, booking.Customer.FirstName),
                    Customer = booking.Customer,
                    TotalTimeSpent = currDate.Value.Subtract(booking.RentedDate),
                    Bikes = _bikes,
                    Customers = _customers,
                    ModifiedBy = booking.ModifiedBy,
                    ModifiedDate = booking.ModifiedDate
                };

                return bookingDisplay;
            }

            return null;
        }

        public async Task<IEnumerable<BikeBookingViewModel>> GetBookings()
        {
            var bookings = await _context.BicycleRentals.AsNoTracking()
                .Include(c => c.Customer)
                .Include(b => b.BicycleInventory)
                .ToListAsync();

            if(bookings != null)
            {
                var bookingsDisplay = new List<BikeBookingViewModel>();
                foreach(var booking in bookings)
                {
                    var currDate = booking.ReturnedDate.HasValue ? booking.ReturnedDate : DateTime.Now;
                    var status = booking.ReturnedDate.HasValue ? "Returned" : "Rented";

                    var bookingDisplay = new BikeBookingViewModel
                    {
                        RentalId = booking.RentalId,
                        RentedDate = booking.RentedDate,
                        ReturnedDate = booking.ReturnedDate,
                        Status = status,
                        SelectedBikeId = booking.BicycleInventory.BikeId,
                        SelectedBikeModel = booking.BicycleInventory.ModelNo,
                        BicycleInventory = booking.BicycleInventory,
                        SelectedCustomerId = booking.Customer.CustomerId,
                        CustomerFullName = string.Format("{0}, {1}", booking.Customer.LastName, booking.Customer.FirstName),
                        Customer = booking.Customer,
                        TotalTimeSpent = currDate.Value.Subtract(booking.RentedDate),
                        Bikes = _bikes,
                        Customers = _customers,
                        ModifiedBy = booking.ModifiedBy,
                        ModifiedDate = booking.ModifiedDate
                    };

                    bookingsDisplay.Add(bookingDisplay);
                }

                return bookingsDisplay;
            }

            return null;
        }

        public async Task<bool> SaveBooking(BikeBookingViewModel model)
        {
            if (model != null)
            {
                var booking = new BicycleBooking
                {
                    RentalId = model.RentalId,
                    RentedDate = model.RentedDate,
                    BicycleInventory = await _context.BicycleInventories.FindAsync(model.SelectedBikeId),
                    Customer = await _context.Customers.FindAsync(model.SelectedCustomerId)
                };

                _context.BicycleRentals.Add(booking);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateBooking(BikeBookingViewModel model)
        {
            if (model != null)
            {
                var booking = new BicycleBooking
                {
                    RentalId = model.RentalId,
                    RentedDate = model.RentedDate,
                    ReturnedDate = model.ReturnedDate,
                    BicycleInventory = await _context.BicycleInventories.FindAsync(model.SelectedBikeId),
                    Customer = await _context.Customers.FindAsync(model.SelectedCustomerId),
                    ModifiedBy = model.ModifiedBy,
                    ModifiedDate = model.ModifiedDate

                };
                var current = await _context.BicycleRentals.FindAsync(model.RentalId);

                _context.Entry(current).CurrentValues.SetValues(booking);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
