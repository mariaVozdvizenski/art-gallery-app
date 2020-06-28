using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IBasketItemService : IBaseEntityService<BasketItem>
    {
        Task<IEnumerable<BasketItem>> GetBasketItemsForUsersBasketAsync(Guid userId);
        Task<BasketItem> GetBasketItemForUserBasketAsync(Guid id, Guid userId);
        Task<bool> DuplicatePaintingExistsAsync(Painting painting, Guid userId);
        bool CheckForPaintingQuantity(BasketItem basketItem, Painting painting);
        bool CheckThatQuantityIsNotZero(BasketItem basketItem);
    }
}