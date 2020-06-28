using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Microsoft.EntityFrameworkCore;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketItemRepository : IBaseRepository<BasketItem>
    {
        Task<IEnumerable<BasketItem>> GetAllForUserBasketAsync(Guid basketId, object? userId = null
            , bool noTracking = true);

        Task<BasketItem> GetFirstForUserBasketAsync(Guid id, Guid basketId, object? userId = null, bool noTracking = true);
    }
}