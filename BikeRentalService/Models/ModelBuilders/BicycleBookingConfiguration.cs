using BikeRentalService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRentalService.Models.ModelBuilders
{
    public class BicycleBookingConfiguration : IEntityTypeConfiguration<BicycleBooking>
    {
        public void Configure(EntityTypeBuilder<BicycleBooking> builder)
        {
            builder.HasKey(pk => pk.RentalId);

            builder.HasOne(fk => fk.Customer);
            builder.HasOne(fk => fk.BicycleInventory);
        }
    }
}
