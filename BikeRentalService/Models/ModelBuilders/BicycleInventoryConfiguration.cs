using BikeRentalService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRentalService.Models.ModelBuilders
{
    public class BicycleInventoryConfiguration : IEntityTypeConfiguration<BicycleInventory>
    {
        public void Configure(EntityTypeBuilder<BicycleInventory> builder)
        {
            builder.HasKey(pk => pk.BikeId);

            builder.HasOne(fk => fk.BicycleType);
            builder.HasMany(fk => fk.BicycleBookings)
                .WithOne(fk => fk.BicycleInventory)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(i => i.Brand);
            builder.HasIndex(i => i.ModelNo);
        }
    }
}
