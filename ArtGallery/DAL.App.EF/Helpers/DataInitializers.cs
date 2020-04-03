using System;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleName = "Admin";
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
            {
                role = new AppRole();
                role.Name = roleName;
                var result = roleManager.CreateAsync(role).Result;
                
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Role creation failed!");
                }
            }

            var userName = "mavozd@mavozd.com";
            var password = "Fake.pass123";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;
                var result = userManager.CreateAsync(user, password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }

            var roleResult = userManager.AddToRoleAsync(user, roleName).Result;
        }
        
        public static void SeedData(AppDbContext context)
        {
            
        }
        
    }
}