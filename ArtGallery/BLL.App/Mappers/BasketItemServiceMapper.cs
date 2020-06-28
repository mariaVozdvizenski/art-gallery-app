using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BasketItem = BLL.App.DTO.BasketItem;

namespace BLL.App.Mappers
{
    public class BasketItemServiceMapper :  AppServiceBaseMapper<DAL.App.DTO.BasketItem, BasketItem>, IBasketItemServiceMapper
    {
        public BasketItemServiceMapper()
        {
        }
    }
}