using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using ShipmentItem = BLL.App.DTO.ShipmentItem;

namespace BLL.App.Mappers
{
    public class ShipmentItemServiceMapper : IShipmentItemServiceMapper
    {
        private readonly OrderItemServiceMapper _orderItemServiceMapper;
        private readonly ShipmentServiceMapper _shipmentServiceMapper;
        public ShipmentItemServiceMapper()
        {
            _orderItemServiceMapper = new OrderItemServiceMapper();
            _shipmentServiceMapper = new ShipmentServiceMapper();
        }
        public ShipmentItem Map(DAL.App.DTO.ShipmentItem inObject)
        {
            return new ShipmentItem()
            {
                Id = inObject.Id,
                OrderItem = _orderItemServiceMapper.Map(inObject.OrderItem!),
                OrderItemId = inObject.OrderItemId,
                Shipment = _shipmentServiceMapper.Map(inObject.Shipment!),
                ShipmentId = inObject.ShipmentId
            };
        }

        public DAL.App.DTO.ShipmentItem Map(ShipmentItem inObject)
        {
            return new DAL.App.DTO.ShipmentItem()
            {
                Id = inObject.Id,
                OrderItem = _orderItemServiceMapper.Map(inObject.OrderItem!),
                OrderItemId = inObject.OrderItemId,
                Shipment = _shipmentServiceMapper.Map(inObject.Shipment!),
                ShipmentId = inObject.ShipmentId
            };
        }
    }
}