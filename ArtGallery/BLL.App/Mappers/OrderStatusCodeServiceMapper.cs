using System.Linq;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using OrderStatusCode = BLL.App.DTO.OrderStatusCode;

namespace BLL.App.Mappers
{
    public class OrderStatusCodeServiceMapper : AppServiceBaseMapper<DAL.App.DTO.OrderStatusCode, OrderStatusCode>, 
        IOrderStatusCodeServiceMapper
    {
    }
}