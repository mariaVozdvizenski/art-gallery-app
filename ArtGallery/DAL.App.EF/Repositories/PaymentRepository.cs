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
    public class PaymentRepository : EFBaseRepository<AppDbContext, Domain.Payment, DTO.Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Payment, DTO.Payment>())
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId)
            }
            
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<DTO.Payment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Invoice)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Payment> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Invoice)
                .Where(p => p.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var payment = await FirstOrDefaultAsync(id, userId);
            base.Remove(payment);
        }
    }
}