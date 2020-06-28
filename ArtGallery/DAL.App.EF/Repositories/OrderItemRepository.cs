using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderItemRepository : EFBaseRepository<AppDbContext, AppUser, OrderItem, DAL.App.DTO.OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext, new OrderItemRepositoryMapper())
        {
        }
        public override async Task<IEnumerable<DTO.OrderItem>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Order)
                .Include(b => b.Painting);
            var domainItems = await query.ToListAsync();
            
            var result = domainItems.Select(b => Mapper.Map(b));
            return result;
        }

        public override async Task<DTO.OrderItem> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            var domainItem = await query
                .Include(b => b.Order)
                .Include(b => b.Painting)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(domainItem);        
        }
    }
}