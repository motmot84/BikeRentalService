using BikeRentalService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeRentalService.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BikeBookingViewModel>> GetBookings();

        Task<BikeBookingViewModel> GetBooking(Guid? id);

        Task<bool> UpdateBooking(BikeBookingViewModel model);

        BikeBookingViewModel CreateBooking();

        Task<bool> SaveBooking(BikeBookingViewModel model);
    }
}