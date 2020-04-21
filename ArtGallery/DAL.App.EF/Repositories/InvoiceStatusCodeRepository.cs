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
    public class InvoiceStatusCodeRepository : EFBaseRepository<AppDbContext, Domain.InvoiceStatusCode, DAL.App.DTO.InvoiceStatusCode>, IInvoiceStatusCodeRepository
    {
        public InvoiceStatusCodeRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<InvoiceStatusCode, DTO.InvoiceStatusCode>())
        {
        }
        public async Task<IEnumerable<DAL.App.DTO.InvoiceStatusCode>> AllAsync(Guid? userId = null)
        {
            if (userId != null)
            {
                //return (await RepoDbSet.Where(i => i.AppUser == userId).ToListAsync())
                    //.Select(domainEntity => Mapper.Map(domainEntity));
            }

            return (await RepoDbSet.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.InvoiceStatusCode> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
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
            var invoiceStatusCode = await FirstOrDefaultAsync(id, userId);
            base.Remove(invoiceStatusCode);
        }
    }
}