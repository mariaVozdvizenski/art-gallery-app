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
    public class ShipmentRepository : EFBaseRepository<AppDbContext, Domain.Shipment, DTO.Shipment>, IShipmentRepository
    {
        public ShipmentRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Shipment, DTO.Shipment>())
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

        public async Task<IEnumerable<DTO.Shipment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Invoice)
                .Include(si => si.Order)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(si => si.AppUserId == userId);
            }
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Shipment> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Invoice)
                .Include(si => si.Order)
                .Where(si => si.Id == id)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(si => si.AppUserId == userId);
            }
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var shipment = await FirstOrDefaultAsync(id, userId);
            base.Remove(shipment);
        }
    }
}