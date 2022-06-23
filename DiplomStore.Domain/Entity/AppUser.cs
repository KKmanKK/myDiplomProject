using Microsoft.AspNetCore.Identity;
namespace DiplomStore.Domain.Entity
{
    public class AppUser:IdentityUser
    {
        public List<Order> Orders { get; set; } 
    }
}
