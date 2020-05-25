using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository :  EFBaseRepository<AppDbContext, AppUser, Invoice, DAL.App.DTO.Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Invoice, DTO.Invoice>())
        {
        }
        
        public override async Task<IEnumerable<DTO.Invoice>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Order);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(b => Mapper.Map(b));
            return result;
        }
        
    }
}