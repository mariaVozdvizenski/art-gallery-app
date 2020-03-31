using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ShipmentItemRepository : EFBaseRepository<ShipmentItem, AppDbContext>, IShipmentItemRepository
    {
        public ShipmentItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(si => si.Id == id);
        }

        public async Task<IEnumerable<ShipmentItem>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.OrderItem)
                .Include(si => si.Shipment)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<ShipmentItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.OrderItem)
                .Include(si => si.Shipment)
                .Where(si => si.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }
    }
}