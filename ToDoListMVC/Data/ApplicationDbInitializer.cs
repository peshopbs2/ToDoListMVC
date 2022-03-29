using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.Models.Identity;

namespace ToDoListMVC.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(roleManager.FindByNameAsync("Admin").Result == null)
            {
                roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
            }

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "adminpassword").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
