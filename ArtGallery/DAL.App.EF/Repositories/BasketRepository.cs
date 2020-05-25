using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Basket = Domain.App.Basket;

namespace DAL.App.EF.Repositories
{
    public class BasketRepository : EFBaseRepository<AppDbContext, AppUser, Basket, DAL.App.DTO.Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext, new BasketRepositoryMapper())
        {
        }

        public override async Task<IEnumerable<DTO.Basket>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Include(b => b.AppUser);
            return await query.Select(e => Mapper.Map(e)).ToListAsync();
        }

        public override async Task<DTO.Basket> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainBasket = await query
                .Include(b => b.AppUser)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            return Mapper.Map(domainBasket);
        }
    }
}