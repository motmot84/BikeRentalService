using BikeRentalService.Models.ViewModels;
using BikeRentalService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BikeRentalService.Controllers
{
    [Authorize(Roles = "Owner")]
    public class BikeController : Controller
    {
        private readonly IBikeRepository _bikeRepo;
        public BikeController(IBikeRepository bikeRepo)
        {
            _bikeRepo = bikeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var bikes = await _bikeRepo.GetBikes();

            if (bikes == null)
                return NoContent();

            return View(bikes);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            var bike = await _bikeRepo.GetBike(id);

            if (bike == null)
                return NotFound();

            return View(bike);
        }

        public async Task<IActionResult> Detail(Guid? id)
        {
            var bike = await _bikeRepo.GetBike(id);

            if (bike == null)
                return NotFound();

            return View(bike);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind(include: new string[] { "BikeId", "ModelNo", "Brand", "Status", "SelectedBikeType", "SelectBikeTypeId" })] BikeViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _bikeRepo.UpdateBike(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }

        public IActionResult Create()
        {
            var bike = _bikeRepo.CreateBike();

            return View(bike);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(include: new string[] { "BikeId", "ModelNo", "Brand", "Status", "SelectedBikeType", "SelectBikeTypeId" })] BikeViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _bikeRepo.SaveBike(model);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            var bike = await _bikeRepo.GetBike(id);

            if (bike == null)
                return NotFound();

            return View(bike);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid? BikeId)
        {
            if (ModelState.IsValid)
            {
                var response = await _bikeRepo.DeleteBike(BikeId);

                if (response)
                    return RedirectToAction("Index");
            }

            return NoContent();
        }
        
        public void AssembleBike(Guid? BikeId)
        {
            while (true)
            {
                break;
            }
        }
    }
}
