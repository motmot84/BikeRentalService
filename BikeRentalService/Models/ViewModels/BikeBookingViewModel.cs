using BikeRentalService.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalService.Models.ViewModels
{
    public class BikeBookingViewModel
    {
        public Guid RentalId { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        [Display(Name = "Check Out Time")]
        public DateTime RentedDate { get; set; }
        [Display(Name = "Check In Time")]
        public DateTime? ReturnedDate { get; set; }
        [Display(Name = "Total Time Spent")]
        public TimeSpan TotalTimeSpent { get; set; }
        public BicycleInventory BicycleInventory { get; set; }
        public Guid SelectedBikeId { get; set; }
        [Display(Name = "Model")]
        public string SelectedBikeModel { get; set; }
        public IEnumerable<SelectListItem> Bikes { get; set; }
        public Customer Customer { get; set; }
        public Guid SelectedCustomerId { get; set; }
        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
