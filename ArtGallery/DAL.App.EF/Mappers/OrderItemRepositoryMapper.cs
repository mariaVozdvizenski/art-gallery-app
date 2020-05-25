using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class OrderItemRepositoryMapper : IBaseMapper<OrderItem, DTO.OrderItem>
    {
        private readonly ShipmentItemRepositoryMapper _shipmentItemRepositoryMapper;
        private readonly OrderRepositoryMapper _orderRepositoryMapper;
        private readonly PaintingRepositoryMapper _paintingRepositoryMapper;

        
        public OrderItemRepositoryMapper()
        {
            _shipmentItemRepositoryMapper = new ShipmentItemRepositoryMapper();
            _orderRepositoryMapper = new OrderRepositoryMapper();
            _paintingRepositoryMapper = new PaintingRepositoryMapper();
        }
        public DTO.OrderItem Map(OrderItem inObject)
        {
            return new DTO.OrderItem()
            {
                Id = inObject.Id,
                ItemShipments = inObject.ItemShipments.Select(e => _shipmentItemRepositoryMapper.Map(e))
                    .ToList(),
                Order = _orderRepositoryMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };
        }

        public OrderItem Map(DTO.OrderItem inObject)
        {
            return new OrderItem()
            {
                Id = inObject.Id,
                ItemShipments = inObject.ItemShipments.Select(e => _shipmentItemRepositoryMapper.Map(e))
                    .ToList(),
                Order = _orderRepositoryMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };        
        }
    }
}