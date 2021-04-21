using BikeRentalService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BikeRentalService.Models
{
    public class SeedData : ISeedData
    {
        private readonly IServiceProvider _services;
        private BicycleRentalDbContext _context;
        public SeedData(IServiceProvider services)
        {
            _services = services;
            _context = services.GetService<BicycleRentalDbContext>();
        }

        public void SeedDatabase(UserManager<LoginAccount> userManager)
        {
            var bType1 = new BicycleType { BikeTypeId = Guid.NewGuid(), Type = "Road Bike" };
            var bType2 = new BicycleType { BikeTypeId = Guid.NewGuid(), Type = "Mountain Bike" };
            var bType3 = new BicycleType { BikeTypeId = Guid.NewGuid(), Type = "Folding Bike" };
            var bType4 = new BicycleType { BikeTypeId = Guid.NewGuid(), Type = "Electric Bike" };

            var bike1 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Cannondale", ModelNo = "Slate Force 1", Status = "Available", BicycleType = bType1 };
            var bike2 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Cannondale", ModelNo = "Slate Apex 1", Status = "Rented", BicycleType = bType1 };
            var bike3 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Cannondale", ModelNo = "Topstone Apex 1", Status = "Available", BicycleType = bType1 };
            var bike4 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Cannondale", ModelNo = "Topstone Sora", Status = "Available", BicycleType = bType1 };
            var bike5 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Specialized", ModelNo = "Stumpjumper", Status = "Available", BicycleType = bType2 };
            var bike6 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Specialized", ModelNo = "Stumpjumper Evo", Status = "Available", BicycleType = bType2 };
            var bike7 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Specialized", ModelNo = "Enduro", Status = "Available", BicycleType = bType2 };
            var bike8 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Specialized", ModelNo = "Fuse", Status = "Available", BicycleType = bType2 };
            var bike9 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Dahon", ModelNo = "Mariner D8", Status = "Available", BicycleType = bType3 };
            var bike10 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Dahon", ModelNo = "Launch D8", Status = "Available", BicycleType = bType3 };
            var bike11 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Dahon", ModelNo = "Suv D6", Status = "Available", BicycleType = bType3 };
            var bike12 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Dahon", ModelNo = "Piazza D7", Status = "Available", BicycleType = bType3 };
            var bike13 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Pinarello", ModelNo = "Dust 3", Status = "Available", BicycleType = bType4 };
            var bike14 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Pinarello", ModelNo = "Dust 2", Status = "Available", BicycleType = bType4 };
            var bike15 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Pinarello", ModelNo = "Nytro", Status = "Available", BicycleType = bType4 };
            var bike16 = new BicycleInventory { BikeId = Guid.NewGuid(), Brand = "Pinarello", ModelNo = "Nytro Roadstar", Status = "Available", BicycleType = bType4 };

            var customer1 = new Customer
            {
                FirstName = "Kim",
                LastName = "Jong-Un",
                Address = "North Korea",
                BirthDate = Convert.ToDateTime("1/8/1984"),
                Status = "Active"
            };
            var customer2 = new Customer
            {
                FirstName = "Donald",
                LastName = "Trump",
                Address = "United States",
                BirthDate = Convert.ToDateTime("6/14/1946"),
                Status = "InActive"
            };
            var customer3 = new Customer
            {
                FirstName = "Vladimir",
                LastName = "Putin",
                Address = "Russia",
                BirthDate = Convert.ToDateTime("10/7/1952"),
                Status = "Active"
            };
            var customer4 = new Customer
            {
                FirstName = "Rodrigo",
                LastName = "Duterte",
                Address = "Philippines",
                BirthDate = Convert.ToDateTime("3/28/1945"),
                Status = "Active"
            };

            if (_context.Database.GetMigrations().Count() > 0
                && _context.Database.GetPendingMigrations().Count() == 0
                && _context.BicycleInventories.Count() == 0)
            {
                _context = _services.GetService<BicycleRentalDbContext>();

                _context.Add(bType1);
                _context.Add(bType2);
                _context.Add(bType3);
                _context.Add(bType4);

                _context.Add(bike1);
                _context.Add(bike2);
                _context.Add(bike3);
                _context.Add(bike4);
                _context.Add(bike5);
                _context.Add(bike6);
                _context.Add(bike7);
                _context.Add(bike8);
                _context.Add(bike9);
                _context.Add(bike10);
                _context.Add(bike11);
                _context.Add(bike12);
                _context.Add(bike13);
                _context.Add(bike14);
                _context.Add(bike15);
                _context.Add(bike16);

                _context.SaveChanges();
            }

            if (_context.Database.GetMigrations().Count() > 0
                   && _context.Database.GetPendingMigrations().Count() == 0
                   && _context.Customers.Count() == 0)
            {
                _context = _services.GetService<BicycleRentalDbContext>();

                _context.Add(customer1);
                _context.Add(customer2);
                _context.Add(customer3);
                _context.Add(customer4);

               _context.SaveChanges();
            }

            if (_context.Database.GetMigrations().Count() > 0
                   && _context.Database.GetPendingMigrations().Count() == 0
                   && _context.Roles.Count() == 0)
            {
                _context = _services.GetService<BicycleRentalDbContext>();

                var roles = new List<UserRole>()
                {
                    new UserRole { Id = Guid.NewGuid(), Name = "Owner", NormalizedName = "OWNER" },
                    new UserRole { Id = Guid.NewGuid(), Name = "Staff", NormalizedName = "STAFF" }
                };

                var logins = new List<LoginAccount>()
                {
                    new LoginAccount
                    {
                        Email = "email123@mail.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "EMAIL123@MAIL.COM",
                        UserName = "email123@mail.com",
                        NormalizedUserName = "EMAIL123@MAIL.COM",
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new LoginAccount
                    {
                        Email = "email456@mail.com",
                        EmailConfirmed = true,
                        NormalizedEmail = "EMAIL456@MAIL.COM",
                        UserName = "email456@mail.com",
                        NormalizedUserName = "EMAIL456@MAIL.COM",
                        SecurityStamp = Guid.NewGuid().ToString()
                    }
                };

                var roleNames = new string[] { "Owner", "Staff" };

                for (int i = 0; i < roles.Count; i++)
                {
                    var roleStore = new RoleStore<UserRole, BicycleRentalDbContext, Guid>(_context);

                    roleStore.CreateAsync(roles[i]);
                }

                for (int i = 0; i < logins.Count; i++)
                {
                    var p = userManager.CreateAsync(logins[i], "Secret123-").Result;
                    var r = userManager.AddToRoleAsync(logins[i], roleNames[i]).Result;
                    Thread.Sleep(1000);
                    _context.SaveChanges();
                }
            }

        }
    }
}
