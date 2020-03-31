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
    public class ShipmentRepository : EFBaseRepository<Shipment, AppDbContext>, IShipmentRepository
    {
        public ShipmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(si => si.Id == id);
        }

        public async Task<IEnumerable<Shipment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Invoice)
                .Include(si => si.Order)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Shipment> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Invoice)
                .Include(si => si.Order)
                .Where(si => si.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }
    }
}