using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentItemRepository : EFBaseRepository<AppDbContext, Domain.ShipmentItem, DTO.ShipmentItem>, IShipmentItemRepository
    {
        public ShipmentItemRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<ShipmentItem, DTO.ShipmentItem>())
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(si => si.Id == id && si.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(si => si.Id == id);
        }

        public async Task<IEnumerable<DTO.ShipmentItem>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.OrderItem)
                .Include(si => si.Shipment)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(si => si.AppUserId == userId);
            }
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.ShipmentItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.OrderItem)
                .Include(si => si.Shipment)
                .Where(si => si.Id == id)
                .AsQueryable();

            if (userId != null)
            {
               // query = query.Where(si => si.AppUserId == userId);
            }
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var shipmentItem = await FirstOrDefaultAsync(id, userId);
            base.Remove(shipmentItem);
        }
    }
}