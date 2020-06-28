using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL.App.EF.Helpers
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

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<Domain.App.Identity.AppRole> roleManager,
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
                AddDefaultBasket(user, context);
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
            var categories = new Category[]
            {
                new Category()
                {
                    CategoryName = "Oil painting",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Category()
                {
                    CategoryName = "Watercolor painting",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Category()
                {
                    CategoryName = "Pastel painting",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new Category()
                {
                    CategoryName = "Acrylic painting",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                },
                new Category()
                {
                    CategoryName = "Digital painting",
                    Id = new Guid("00000000-0000-0000-0000-000000000005")
                }
            };
            
            AddDataToDb(categories, context);
            
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    PaymentMethodCode = "PayPal",
                    PaymentMethodDescription = 
                        "With PayPal, customers can send payments securely online using a stored value account " +
                        "that is linked to a credit card, Signature (PINless) debit card or bank account.",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new PaymentMethod()
                {
                    PaymentMethodCode = "Credit Card",
                    PaymentMethodDescription = 
                        "Purchasers can use credit cards to buy goods based on the card holder’s " +
                        "promise to pay for these goods and services.",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
            };
            
            AddDataToDb(paymentMethods, context);

            var orderStatusCodes = new OrderStatusCode[]
            {
                new OrderStatusCode()
                {
                    Code = "Shipped",
                    OrderStatusDescription = "The order has been successfully paid for and shipped.",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new OrderStatusCode()
                {
                    Code = "Processing",
                    OrderStatusDescription = "The order has been successfully paid for and is waiting to be shipped.",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new OrderStatusCode()
                {
                    Code = "Delivered",
                    OrderStatusDescription = "The customer has received the ordered product.",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new OrderStatusCode()
                {
                    Code = "Cancelled",
                    OrderStatusDescription = "The order has been cancelled.",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                },
            };
            
            AddDataToDb(orderStatusCodes, context);
            
            var invoiceStatusCodes = new InvoiceStatusCode[]
            {
                new InvoiceStatusCode()
                {
                    Code = "Sent",
                    InvoiceStatusDescription = "The invoice has been sent to the user.",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new InvoiceStatusCode()
                {
                    Code = "Paid",
                    InvoiceStatusDescription = "The invoice has been paid for.",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
            };
            
            AddDataToDb(invoiceStatusCodes, context);
        }

        private static async void AddDefaultBasket(AppUser appUser, AppDbContext context)
        {
            var basket = new Basket()
            {
                AppUser = appUser,
                AppUserId = appUser.Id,
                ChangedAt = DateTime.Now,
                ChangedBy = appUser.Email,
                CreatedAt = DateTime.Now,
                CreatedBy = appUser.Email,
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                DateCreated = DateTime.Now,
            };
            
            var baskets = context.Set<Basket>();

            if (!baskets.Any(b => b.Id == basket.Id))
            {
               await baskets.AddAsync(basket);
            }
            await context.SaveChangesAsync();
        }
    }
}