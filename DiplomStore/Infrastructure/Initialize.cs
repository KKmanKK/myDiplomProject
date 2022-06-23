using DiplomStore.Domain.Entity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DiplomStore.Infrastructure
{
    public static class Initialize
    {
        public static async void Init(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if(await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if(await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };
            
            if(await userManager.FindByNameAsync(admin.UserName) == null)
            {
                var result = await userManager.CreateAsync(admin, "Admin1234");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            
            
           
        }
    }
}
