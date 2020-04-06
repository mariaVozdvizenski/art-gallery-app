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
    public class OrderItemRepository : EFBaseRepository<OrderItem, AppDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Painting)
                .AsQueryable();
            /*if (userId != null)
            {
                query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }*/
            return await query.ToListAsync();
        }

        public async Task<OrderItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Painting)
                .Where(b => b.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }
    }
}