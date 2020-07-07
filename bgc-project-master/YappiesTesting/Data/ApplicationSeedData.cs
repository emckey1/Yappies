using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Data
{
    public static class ApplicationSeedData
    {
        
      public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Program Supervisor", "Parent" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("programsup@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "programsup@gmail.com",
                    Email = "programsup@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Program Supervisor").Wait();
                }
            }
            if (userManager.FindByEmailAsync("parent@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "parent@gmail.com",
                    Email = "parent@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Parent").Wait();
                }
            }

           if (userManager.FindByEmailAsync("kdall1@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "kdall1@hotmail.com",
                    Email = "kdall1@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Program Supervisor").Wait();
                }
            }
            if (userManager.FindByEmailAsync("taxidriver@hotmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "taxidriver@hotmail.com",
                    Email = "taxidriver@hotmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Parent").Wait();
                }
            }
        }

    }
}
