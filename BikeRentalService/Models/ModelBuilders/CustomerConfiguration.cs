using BikeRentalService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeRentalService.Models.ModelBuilders
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(pk => pk.CustomerId);

            builder.HasMany(fk => fk.BicycleBookings)
                .WithOne(fk => fk.Customer)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(i => i.FirstName);
            builder.HasIndex(i => i.LastName);
        }
    }
}
