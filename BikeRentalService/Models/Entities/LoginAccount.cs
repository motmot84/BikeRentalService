using Microsoft.AspNetCore.Identity;
using System;

namespace BikeRentalService.Models.Entities
{
    public class LoginAccount : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }
    }
}
