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
    public class InvoiceRepository :  EFBaseRepository<Invoice, AppDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> ExsistsAsync(Guid id, Guid? userId = null)
        {
            /*if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }*/
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<Invoice>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.InvoiceStatusCode)
                .Include(b => b.Order)
                .AsQueryable();
            /*if (userId != null)
            {
                query = query.Where(o => o.Owner!.AppUserId == userId && o.Animal!.AppUserId == userId);
            }*/
            return await query.ToListAsync();
        }

        public async Task<Invoice> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.InvoiceStatusCode)
                .Include(b => b.Order)
                .Where(a => a.Id == id)
                .AsQueryable();
            /*if (userId != null)
            {
                query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }*/
            return await query.FirstOrDefaultAsync();
        }
        /*public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }*/
    }
}