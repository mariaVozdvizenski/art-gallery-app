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
            var query = RepoDbSet
                .Include(u => u.AppUser)
                .Where(b => b.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Basket>> AllAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(u => u.AppUser)
                .AsQueryable();
            return await query.ToListAsync();        
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await RepoDbContext.Users.ToListAsync();
        }
    }
}