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
    public class UserPaymentMethodRepository : EFBaseRepository<UserPaymentMethod, AppDbContext>,
        IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<UserPaymentMethod>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .Include(up => up.PaymentMethod)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<UserPaymentMethod> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .Include(up => up.PaymentMethod)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a =>
                a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }

    }
}
