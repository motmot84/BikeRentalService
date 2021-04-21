using Microsoft.AspNetCore.Identity;
using System;

namespace BikeRentalService.Models.Entities
{
    public class UserRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
