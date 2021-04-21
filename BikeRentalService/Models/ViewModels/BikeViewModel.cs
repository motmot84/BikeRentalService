using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalService.Models.ViewModels
{
    public class BikeViewModel
    {
        public Guid BikeId { get; set; }
        [Display(Name = "Model")]
        [Required]
        public string ModelNo { get; set; }
        public string Brand { get; set; }
        [Required]
        public string Status { get; set; }
        public IEnumerable<SelectListItem> BikeStatuses { get; set; }
        [Display(Name = "Bike Type")]
        public string SelectedBikeType { get; set; }
        [Required]
        public Guid SelectBikeTypeId { get; set; }
        public IEnumerable<SelectListItem> BikeTypes { get; set; }
    }
}
