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
    public class ShipmentRepository : EFBaseRepository<AppDbContext, AppUser, Shipment, DTO.Shipment>, IShipmentRepository
    {
        public ShipmentRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Shipment, DTO.Shipment>())
        {
        }

        public override async Task<IEnumerable<DTO.Shipment>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(e => e.Invoice)
                .Include(e => e.Order);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));

            return result;
        }
    }
}