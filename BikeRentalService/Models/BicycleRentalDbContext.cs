using BikeRentalService.Models.Entities;
using BikeRentalService.Models.ModelBuilders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BikeRentalService.Models
{
    public class BicycleRentalDbContext : IdentityDbContext<LoginAccount, UserRole, Guid>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BicycleInventory> BicycleInventories { get; set; }
        public DbSet<BicycleType> BicycleTypes { get; set; }
        public DbSet<BicycleBooking> BicycleRentals { get; set; }

        public BicycleRentalDbContext(DbContextOptions<BicycleRentalDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new LoginAccountConfiguration());
            //modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new BicycleInventoryConfiguration());
            modelBuilder.ApplyConfiguration(new BicycleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BicycleBookingConfiguration());
        }
    }
}
