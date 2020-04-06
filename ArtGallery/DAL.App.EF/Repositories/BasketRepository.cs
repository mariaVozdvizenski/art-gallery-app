using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BasketRepository : EFBaseRepository<Basket, AppDbContext>, IBasketRepository
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Basket> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id)
                .Include(a => a.AppUser)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task<IEnumerable<Basket>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return await query.ToListAsync();
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var basket = await FirstOrDefaultAsync(id, userId);
            base.Remove(basket);
        }
    }
}