using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BikeRentalService.Models.Entities
{
    public class BicycleType
    {
        public Guid BikeTypeId { get; set; }
        public string Type { get; set; }
        public IEnumerable<BicycleInventory> BicycleInventories { get; set; }
    }
}
