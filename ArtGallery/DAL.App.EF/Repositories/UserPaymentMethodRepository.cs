using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class UserPaymentMethodRepository : EFBaseRepository<AppDbContext, Domain.UserPaymentMethod, DTO.UserPaymentMethod>,
        IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<UserPaymentMethod, DTO.UserPaymentMethod>())
        {
        }
        
        public async Task<IEnumerable<DTO.UserPaymentMethod>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .Include(up => up.PaymentMethod)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.UserPaymentMethod> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
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

            return Mapper.Map(await query.FirstOrDefaultAsync());
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
            var userPaymentMethod = await FirstOrDefaultAsync(id, userId);
            base.Remove(userPaymentMethod);
        }
        /*
        public async Task<IEnumerable<UserPaymentMethodDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query =  RepoDbSet.AsQueryable();

            if (userId != null)
            {
                query = query.Where(p => p.AppUserId == userId);
            }

            return await query.Select(p => new UserPaymentMethodDTO()
            {
                Id = p.Id,
                UserPaymentMethod = p.PaymentMethod.PaymentMethodCode,
            }).ToListAsync();
        }

        public Task<UserPaymentMethodDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query =  RepoDbSet.Where(p => p.Id == id).AsQueryable();

            if (userId != null)
            {
                query = query.Where(p => p.AppUserId == userId);
            }

            var userPaymentDTO = query.Select(p => new UserPaymentMethodDTO()
            {
                Id = p.Id,
                UserPaymentMethod = p.PaymentMethod.PaymentMethodCode
            }).FirstOrDefaultAsync();

            return userPaymentDTO;
        }
        */
    }
}
