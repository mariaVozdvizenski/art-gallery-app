using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Order = Domain.App.Order;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<AppDbContext, AppUser, Order, DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Order, DTO.Order>())
        {
        }

        public override async Task<IEnumerable<DTO.Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(o => o.AppUser);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(o => Mapper.Map(o));

            return result;
        }
    }
}