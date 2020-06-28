using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class BasketItemRepository : EFBaseRepository<AppDbContext, AppUser, BasketItem, DAL.App.DTO.BasketItem>,
        IBasketItemRepository
    {
        public BasketItemRepository(AppDbContext dbContext) : base(dbContext,
            new BasketItemRepositoryMapper())
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

        public override async Task<DTO.BasketItem> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            var basketItem = await query
                .Include(b => b.Basket)
                .Include(b => b.Painting)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(basketItem);
        }

        public async Task<IEnumerable<DTO.BasketItem>> GetAllForUserBasketAsync(Guid basketId, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Basket)
                .Include(b => b.Painting)
                .Where(b => b.BasketId.Equals(basketId));

            var domainItems = await query.ToListAsync();
            
            var result = domainItems.Select(d => Mapper.Map(d));

            return result;
        }
        
        public async Task<DTO.BasketItem> GetFirstForUserBasketAsync(Guid id, Guid basketId, object? userId = null, 
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(b => b.Basket)
                .Include(b => b.Painting)
                .Where(b => b.BasketId.Equals(basketId));

            var domainBasketItem = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            
            return Mapper.Map(domainBasketItem);
        }
        
    }
}