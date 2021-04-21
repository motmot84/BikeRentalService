using System;

namespace BikeRentalService.Models.Entities
{
    public class BicycleBooking
    {
        public Guid RentalId { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public BicycleInventory BicycleInventory { get; set; }
        public Customer Customer { get; set; }
    }
}
