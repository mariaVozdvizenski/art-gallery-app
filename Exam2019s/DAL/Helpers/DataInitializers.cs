using System;
using System.Linq;
using Domain;
using Domain.Identity;
using ee.itcollege.mavozd.Contracts.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            AppDbContext context)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }


            var users = new (string name, string password, Guid Id)[]
            {
                ("mavozd@mavozd.com", "Fake.pass123", new Guid("00000000-0000-0000-0000-000000000001")),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }
        
        private static async void AddDataToDb<TEntity> (TEntity[] entities, AppDbContext context)
        where TEntity : class, IDomainEntityId
        {
            DbSet<TEntity> entityDbSet = context.Set<TEntity>();
            
            foreach (var entity in entities)
            {
                if (!entityDbSet.Any(l => l.Id == entity.Id))
                {
                    await entityDbSet.AddAsync(entity);
                }
            }
            await context.SaveChangesAsync();
        }

        public static void SeedData(AppDbContext context)
        {
            var quizTypes = new QuizType[]
            {
                new QuizType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Type = "Poll"
                },
                new QuizType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Type = "Quiz"
                },
            };
            
            foreach (var entity in quizTypes)
            {
                if (!context.QuizTypes.Any(l => l.Id == entity.Id))
                {
                     context.QuizTypes.Add(entity);
                }
            }
            context.SaveChanges();
        }
    }
}