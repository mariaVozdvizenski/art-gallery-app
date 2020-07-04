using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.Domain;
using Domain.App;
using Domain.App.Identity;
using ee.itcollege.mavozd.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        public DbSet<Address> Addresses { get; set; } = default!;
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
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; } = default!;

        private IUserNameProvider _userNameProvider;
        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)  
        {
            _userNameProvider = userNameProvider;
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Painting>()
                .HasMany(p => p.PaintingCategories)
                .WithOne(pc => pc.Painting!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Painting>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Painting!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Painting>()
                .HasMany(p => p.ItemBaskets)
                .WithOne(bi => bi.Painting!)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}