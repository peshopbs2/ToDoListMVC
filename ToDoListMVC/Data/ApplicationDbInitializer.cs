using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.Models.Identity;

namespace ToDoListMVC.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            var adminRoleName = configuration.GetSection("AdminUser").GetSection("RoleName").Value;

            if (roleManager.FindByNameAsync(adminRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole()
                {
                    Name = adminRoleName
                });
            }

            if (roleManager.FindByNameAsync("RegularUser").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "RegularUser"
                });
            }

            var adminUsername = configuration.GetSection("AdminUser").GetSection("Username").Value;
            var adminPassword = configuration.GetSection("AdminUser").GetSection("Password").Value;
            var adminEmail = configuration.GetSection("AdminUser").GetSection("Email").Value;
            if (userManager.FindByNameAsync(adminUsername).Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = adminUsername,
                    Email = adminEmail
                };

                IdentityResult result = userManager.CreateAsync(user, adminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, adminRoleName).Wait();
                }
            }
        }
    }
}
