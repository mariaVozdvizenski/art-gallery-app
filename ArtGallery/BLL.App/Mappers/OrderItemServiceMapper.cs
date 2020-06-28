using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Order = BLL.App.DTO.Order;
using OrderItem = BLL.App.DTO.OrderItem;

namespace BLL.App.Mappers
{
    public class OrderItemServiceMapper : AppServiceBaseMapper<DAL.App.DTO.OrderItem, OrderItem>, IOrderItemServiceMapper
    {
    }
}