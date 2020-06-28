using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using BasketItem = DAL.App.DTO.BasketItem;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class BasketItemService :
        BaseEntityService<IAppUnitOfWork, IBasketItemRepository, IBasketItemServiceMapper, BasketItem, BLLAppDTO.BasketItem>
        , IBasketItemService
    {
        public BasketItemService(IAppUnitOfWork uow) 
            : base(uow, uow.BasketItems, new BasketItemServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.BasketItem>> GetBasketItemsForUsersBasketAsync(Guid userId)
        {
            var basket = await UOW.Baskets.GetRightBasketForUserAsync(userId);
            var dalBasketItems = await Repository.GetAllForUserBasketAsync(basket.Id, userId);
            return dalBasketItems.Where(e => e.Painting!.Quantity > 0).Select(e => Mapper.Map(e)).ToList();
        }

        public async Task<BLLAppDTO.BasketItem> GetBasketItemForUserBasketAsync(Guid id, Guid userId)
        {
            var basket = await UOW.Baskets.GetRightBasketForUserAsync(userId);
            var dalBasketItem = await Repository.GetFirstForUserBasketAsync(id, basket.Id, userId);
            return Mapper.Map(dalBasketItem);
        }

        public async Task<bool> DuplicatePaintingExistsAsync(Painting painting, Guid userId)
        {
            var basket = await UOW.Baskets.GetRightBasketForUserAsync(userId);
            var dalBasketItems = await Repository.GetAllForUserBasketAsync(basket.Id, userId);
            return dalBasketItems.Any(e => e.PaintingId == painting.Id);
        }

        public bool CheckForPaintingQuantity(BLLAppDTO.BasketItem basketItem, BLLAppDTO.Painting painting)
        {
            return basketItem.Quantity <= painting!.Quantity;
        }

        public bool CheckThatQuantityIsNotZero(BLLAppDTO.BasketItem basketItem)
        {
            return basketItem.Quantity > 0;
        }
    }
}