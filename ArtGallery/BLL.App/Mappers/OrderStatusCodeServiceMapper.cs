using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using OrderStatusCode = BLL.App.DTO.OrderStatusCode;

namespace BLL.App.Mappers
{
    public class OrderStatusCodeServiceMapper : IOrderStatusCodeServiceMapper
    {
        private readonly OrderServiceMapper _orderServiceMapper;
        public OrderStatusCodeServiceMapper()
        {
            _orderServiceMapper = new OrderServiceMapper();
        }
        public OrderStatusCode Map(DAL.App.DTO.OrderStatusCode inObject)
        {
            return new OrderStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Orders = inObject.Orders.Select(e => _orderServiceMapper.Map(e)).ToList(),
                OrderStatusDescription = inObject.OrderStatusDescription
            };
        }

        public DAL.App.DTO.OrderStatusCode Map(OrderStatusCode inObject)
        {
            return new DAL.App.DTO.OrderStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Orders = inObject.Orders.Select(e => _orderServiceMapper.Map(e)).ToList(),
                OrderStatusDescription = inObject.OrderStatusDescription
            };
        }
    }
}