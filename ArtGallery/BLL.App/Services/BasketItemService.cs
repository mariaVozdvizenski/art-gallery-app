using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BasketItem = DAL.App.DTO.BasketItem;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class BasketItemService :
        BaseEntityService<IAppUnitOfWork, IBasketItemRepository, IBasketItemServiceMapper, BasketItem, BLLAppDTO.BasketItem>, IBasketItemService
    {
        public BasketItemService(IAppUnitOfWork uow) 
            : base(uow, uow.BasketItems, new BasketItemServiceMapper())
        {
        }
    }
}