using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalService.Models.ViewModels
{
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        
        public string FullName { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
