using BikeRentalService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeRentalService.Repositories
{
    public interface IBikeRepository
    {
        Task<IEnumerable<BikeViewModel>> GetBikes();

        Task<BikeViewModel> GetBike(Guid? id);

        Task<bool> UpdateBike(BikeViewModel model);

        BikeViewModel CreateBike();

        Task<bool> SaveBike(BikeViewModel model);

        Task<bool> DeleteBike(Guid? id);
    }
}
