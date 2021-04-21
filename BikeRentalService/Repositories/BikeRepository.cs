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
    public class BikeRepository : IBikeRepository
    {
        private readonly BicycleRentalDbContext _context;
        private List<SelectListItem> _statuses;
        private List<SelectListItem> _bikeTypes;

        public BikeRepository(BicycleRentalDbContext context)
        {
            _context = context;

            _statuses = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Available", Text = "Available" },
                new SelectListItem { Value = "UnderMaintenance", Text = "UnderMaintenance" },
                new SelectListItem { Value = "OutOfOrder", Text = "OutOfOrder" }
            };

            _bikeTypes = _context.BicycleTypes.AsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.BikeTypeId.ToString(),
                    Text = x.Type
                }).ToList();
        }

        public async Task<IEnumerable<BikeViewModel>> GetBikes()
        {
            var bikes = new List<BicycleInventory>();
            bikes = await _context.BicycleInventories.AsNoTracking()
                .Include(x => x.BicycleType)
                .ToListAsync();

            if (bikes != null)
            {
                var bikesDisplay = new List<BikeViewModel>();
                foreach (var b in bikes)
                {
                    var bikeDisplay = new BikeViewModel()
                    {
                        BikeId = b.BikeId,
                        Brand = b.Brand,
                        ModelNo = b.ModelNo,
                        SelectBikeTypeId = b.BicycleType.BikeTypeId,
                        SelectedBikeType = b.BicycleType.Type,
                        Status = b.Status
                    };

                    bikesDisplay.Add(bikeDisplay);
                }

                return bikesDisplay;
            }

            return null;
        }

        public async Task<BikeViewModel> GetBike(Guid? id)
        {
            var bike = await _context.BicycleInventories.AsNoTracking()
                .Include(x => x.BicycleType)
                .FirstOrDefaultAsync(x => x.BikeId == id);

            if(bike != null)
            {
                var bikeDisplay = new BikeViewModel
                {
                    BikeId = bike.BikeId,
                    Brand = bike.Brand,
                    ModelNo = bike.ModelNo,
                    SelectBikeTypeId = bike.BicycleType.BikeTypeId,
                    SelectedBikeType = bike.BicycleType.Type,
                    Status = bike.Status,
                    BikeStatuses = _statuses,
                    BikeTypes = _bikeTypes
                };

                return bikeDisplay;
            }

            return null;
        }

        public async Task<bool> UpdateBike(BikeViewModel model)
        {
            if(model != null)
            {
                var bike = new BicycleInventory
                {
                    BikeId = model.BikeId,
                    ModelNo = model.ModelNo,
                    Brand = model.Brand,
                    Status = model.Status,
                    BicycleType = await _context.BicycleTypes.FindAsync(model.SelectBikeTypeId)
                };

                var current = await _context.BicycleInventories.FindAsync(model.BikeId);

                _context.Entry(current).CurrentValues.SetValues(bike);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public BikeViewModel CreateBike()
        {
            return new BikeViewModel
            {
                BikeId = Guid.NewGuid(),
                BikeStatuses = _statuses,
                BikeTypes = _bikeTypes
            };
        }

        public async Task<bool> SaveBike(BikeViewModel model)
        {
            if (model != null)
            {
                var bike = new BicycleInventory
                {
                    BikeId = Guid.NewGuid(),
                    ModelNo = model.ModelNo,
                    Brand = model.Brand,
                    Status = model.Status,
                    BicycleType = await _context.BicycleTypes.FindAsync(model.SelectBikeTypeId)
                };

                _context.BicycleInventories.Add(bike);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBike(Guid? id)
        {
            if(id != null)
            {
                var bike = await _context.BicycleInventories.FindAsync(id);

                _context.BicycleInventories.Remove(bike);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
