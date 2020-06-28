using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.mavozd.DAL.Base.EF.Repositories;
using ee.itcollege.mavozd.DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Order = Domain.App.Order;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<AppDbContext, AppUser, Order, DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext, new OrderRepositoryMapper())
        {
        }

        public override async Task<IEnumerable<DTO.Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(o => o.AppUser)
                .Include(e => e.OrderStatusCode)
                .Include(e => e.Address)
                .Include(e => e.OrderItems)
                .ThenInclude(e => e.Painting);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(o => Mapper.Map(o));

            return result;
        }

        public override async Task<DTO.Order> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var domainEntity = await query
                .Include(o => o.AppUser)
                .Include(e => e.OrderStatusCode)
                .Include(e => e.Address)
                .Include(e => e.OrderItems)
                .ThenInclude(e => e.Painting)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(domainEntity);
        }
    }
}