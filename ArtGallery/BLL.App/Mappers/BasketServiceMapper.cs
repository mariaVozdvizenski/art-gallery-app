using System.Linq;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Basket = BLL.App.DTO.Basket;

namespace BLL.App.Mappers
{
    public class BasketServiceMapper : AppServiceBaseMapper<DAL.App.DTO.Basket, Basket>, IBasketServiceMapper
    {
        public BasketServiceMapper()
        {
        }
    }
}