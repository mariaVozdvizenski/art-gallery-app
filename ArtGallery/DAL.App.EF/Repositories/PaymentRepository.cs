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
    public class PaymentRepository : EFBaseRepository<Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Invoice)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Payment> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Invoice)
                .Where(p => p.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }
    }
}