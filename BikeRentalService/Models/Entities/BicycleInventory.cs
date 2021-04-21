using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BikeRentalService.Models.Entities
{
    public class BicycleInventory
    {
        public Guid BikeId { get; set; }
        public string ModelNo { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }

        public BicycleType BicycleType { get; set; }
        public IEnumerable<BicycleBooking> BicycleBookings { get; set; }
    }
}
