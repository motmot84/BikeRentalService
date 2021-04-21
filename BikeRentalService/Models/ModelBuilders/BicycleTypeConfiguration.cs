using BikeRentalService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRentalService.Models.ModelBuilders
{
    public class BicycleTypeConfiguration : IEntityTypeConfiguration<BicycleType>
    {
        public void Configure(EntityTypeBuilder<BicycleType> builder)
        {
            builder.HasKey(pk => pk.BikeTypeId);

            builder.HasMany(fk => fk.BicycleInventories)
                .WithOne(fk => fk.BicycleType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
