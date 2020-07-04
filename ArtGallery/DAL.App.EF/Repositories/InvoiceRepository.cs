using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.mavozd.DAL.Base.EF.Repositories;
using ee.itcollege.mavozd.DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository :  EFBaseRepository<AppDbContext, AppUser, Invoice, DAL.App.DTO.Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext, new InvoiceRepositoryMapper())
        {
        }
        
        public override async Task<IEnumerable<DTO.Invoice>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Order)
                .Include(b => b.Order!.AppUser)
                .Include(b => b.Order!.OrderItems)
                .ThenInclude(oi => oi.Painting)
                .Include(b => b.InvoiceStatusCode);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(b => Mapper.Map(b));
            return result;
        }

        public override async Task<DTO.Invoice> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Order)
                .Include(b => b.Order!.AppUser)
                .Include(b => b.Order!.OrderItems)
                .ThenInclude(oi => oi.Painting)
                .Include(b => b.InvoiceStatusCode)
                .Where(e => e.Id == id);
            
            var domainItem = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainItem);        
        }
    }
}