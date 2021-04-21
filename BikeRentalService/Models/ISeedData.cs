using BikeRentalService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BikeRentalService.Models
{
    public interface ISeedData
    {
        void SeedDatabase(UserManager<LoginAccount> userManager);
    }
}