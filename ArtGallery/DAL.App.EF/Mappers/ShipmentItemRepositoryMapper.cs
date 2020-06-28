using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class ShipmentItemRepositoryMapper : IBaseMapper<ShipmentItem, DTO.ShipmentItem>
    {
        private readonly OrderItemRepositoryMapper _orderItemRepositoryMapper;
        private readonly ShipmentRepositoryMapper _shipmentRepositoryMapper;
        
        public ShipmentItemRepositoryMapper()
        {
          _orderItemRepositoryMapper = new OrderItemRepositoryMapper();  
          _shipmentRepositoryMapper = new ShipmentRepositoryMapper();
        }
        public DTO.ShipmentItem Map(ShipmentItem inObject)
        {
            return new DTO.ShipmentItem()
            {
                Id = inObject.Id,
                OrderItem = _orderItemRepositoryMapper.Map(inObject.OrderItem!),
                OrderItemId = inObject.OrderItemId,
                Shipment = _shipmentRepositoryMapper.Map(inObject.Shipment!),
                ShipmentId = inObject.ShipmentId
            };
        }

        public ShipmentItem Map(DTO.ShipmentItem inObject)
        {
            return new ShipmentItem()
            {
                Id = inObject.Id,
                OrderItem = _orderItemRepositoryMapper.Map(inObject.OrderItem!),
                OrderItemId = inObject.OrderItemId,
                Shipment = _shipmentRepositoryMapper.Map(inObject.Shipment!),
                ShipmentId = inObject.ShipmentId
            };        
        }
    }
}