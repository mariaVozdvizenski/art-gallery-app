using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class BasketRepository : EFBaseRepository<AppDbContext, Domain.Basket, DAL.App.DTO.Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Basket, DAL.App.DTO.Basket>())
        {
        }
        public async Task<DAL.App.DTO.Basket> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id)
                .Include(a => a.AppUser)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }
            
            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task<IEnumerable<DAL.App.DTO.Basket>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var basket = await FirstOrDefaultAsync(id, userId);
            base.Remove(basket);
        }
        
        /*
        public async Task<IEnumerable<BasketDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(o => new BasketDTO()
                {
                    Id = o.Id,
                    DateCreated = o.DateCreated,
                    ItemCount = o.BasketItems.Count
                })
                .ToListAsync();
        }

        public async Task<BasketDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            var basketDTO = query.Select(b => new BasketDTO()
            {
                Id = b.Id,
                DateCreated = b.DateCreated,
                ItemCount = b.BasketItems.Count

            }).FirstOrDefaultAsync();

            return await basketDTO;
        }
        */
    }
}