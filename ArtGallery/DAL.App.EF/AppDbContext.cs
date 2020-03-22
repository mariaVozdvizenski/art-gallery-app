using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<Basket> Baskets { get; set; } = default!;
        public DbSet<BasketItem> BasketItems { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<InvoiceStatusCode> InvoiceStatusCodes { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<OrderStatusCode> OrderStatusCodes { get; set; } = default!;
        public DbSet<Painting> Paintings { get; set; } = default!;
        public DbSet<PaintingCategory> PaintingCategories { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Shipment> Shipments { get; set; } = default!;
        public DbSet<ShipmentItem> ShipmentItems { get; set; } = default!;
        public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; } = default!;

        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}