using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BasketItemRepository : EFBaseRepository<AppDbContext, AppUser, BasketItem, DAL.App.DTO.BasketItem>,
        IBasketItemRepository
    {
        public BasketItemRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<BasketItem, DTO.BasketItem>())
        {
        }

        public override async Task<IEnumerable<DTO.BasketItem>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Basket)
                .Include(b => b.Painting);

            var domainItems = await query.ToListAsync();
            
            var result = domainItems.Select(d => Mapper.Map(d));

            return result;
        }
    }
}