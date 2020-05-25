using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using OrderItem = BLL.App.DTO.OrderItem;

namespace BLL.App.Mappers
{
    public class OrderItemServiceMapper : IOrderItemServiceMapper
    {
        private readonly ShipmentItemServiceMapper _shipmentItemServiceMapper;
        private readonly OrderServiceMapper _orderServiceMapper;
        private readonly PaintingServiceMapper _paintingServiceMapper;

        public OrderItemServiceMapper()
        {
            _shipmentItemServiceMapper = new ShipmentItemServiceMapper();
            _orderServiceMapper = new OrderServiceMapper();
            _paintingServiceMapper = new PaintingServiceMapper();
        }
        public OrderItem Map(DAL.App.DTO.OrderItem inObject)
        {
            return new OrderItem()
            {
                Id = inObject.Id,
                ItemShipments = inObject.ItemShipments
                    .Select(e => _shipmentItemServiceMapper.Map(e)).ToList(),
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };
        }

        public DAL.App.DTO.OrderItem Map(OrderItem inObject)
        {
            return new DAL.App.DTO.OrderItem()
            {
                Id = inObject.Id,
                ItemShipments = inObject.ItemShipments
                    .Select(e => _shipmentItemServiceMapper.Map(e)).ToList(),
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };        
        }
    }
}