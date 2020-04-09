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
    public class OrderStatusCodeRepository : EFBaseRepository<OrderStatusCode, AppDbContext>, IOrderStatusCodeRepository
    {
        public OrderStatusCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderStatusCode>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();

            if (userId != null)
            {
                //query = query.Where(up => up.AppUserId == userId);
            }
            return await query.ToListAsync();
        }

        public Task<OrderStatusCode> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            if (userId != null)
            {
                //query = query.Where(up => up.AppUserId == userId);
            }
            return query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                //return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var orderStatusCode = await FirstOrDefaultAsync(id, userId);
            base.Remove(orderStatusCode);
        }
    }
}