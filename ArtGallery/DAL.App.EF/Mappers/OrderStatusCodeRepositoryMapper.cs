using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class OrderStatusCodeRepositoryMapper : IBaseMapper<OrderStatusCode, DTO.OrderStatusCode>
    {
        private readonly OrderRepositoryMapper _orderRepositoryMapper;
        
        public OrderStatusCodeRepositoryMapper()
        {
            _orderRepositoryMapper = new OrderRepositoryMapper();
        }
        public DTO.OrderStatusCode Map(OrderStatusCode inObject)
        {
            return new DTO.OrderStatusCode()
            {
                Code = inObject.Code,
                Orders = inObject.Orders.Select(e => _orderRepositoryMapper.Map(e)).ToList(),
                Id = inObject.Id, 
                OrderStatusDescription = inObject.OrderStatusDescription
            };
        }

        public OrderStatusCode Map(DTO.OrderStatusCode inObject)
        {
            return new OrderStatusCode()
            {
                Code = inObject.Code,
                Orders = inObject.Orders.Select(e => _orderRepositoryMapper.Map(e)).ToList(),
                Id = inObject.Id, 
                OrderStatusDescription = inObject.OrderStatusDescription
            };
        }
    }
}