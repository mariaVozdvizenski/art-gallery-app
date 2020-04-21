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
    public class PaymentMethodRepository : EFBaseRepository<AppDbContext, Domain.PaymentMethod, DTO.PaymentMethod>, 
        IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<PaymentMethod, DTO.PaymentMethod>())
        {
        }

        public async Task<DTO.PaymentMethod> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId)
            }
            
            return await RepoDbSet.AnyAsync(pm => pm.Id == id);
        }

        public async Task<IEnumerable<DTO.PaymentMethod>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }

            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var paymentMethod = await FirstOrDefaultAsync(id, userId);
            base.Remove(paymentMethod);
        }
    }
}