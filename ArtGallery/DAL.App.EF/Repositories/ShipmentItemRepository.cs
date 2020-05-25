using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentItemRepository : EFBaseRepository<AppDbContext, AppUser, ShipmentItem, DTO.ShipmentItem>, IShipmentItemRepository
    {
        public ShipmentItemRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<ShipmentItem, DTO.ShipmentItem>())
        {
        }

        public override async Task<IEnumerable<DTO.ShipmentItem>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(e => e.Shipment)
                .Include(e => e.OrderItem);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));

            return result;
        }
    }
}