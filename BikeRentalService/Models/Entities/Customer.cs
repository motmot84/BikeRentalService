using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BikeRentalService.Models.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public IEnumerable<BicycleBooking> BicycleBookings { get; set; }
    }
}
