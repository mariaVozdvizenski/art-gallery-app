using System.Linq;
using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class ShipmentRepositoryMapper : IBaseMapper<Shipment, DTO.Shipment>
    {
        private readonly InvoiceRepositoryMapper _invoiceRepositoryMapper;
        private readonly OrderRepositoryMapper _orderRepositoryMapper;
        private readonly ShipmentItemRepositoryMapper _shipmentItemRepositoryMapper;
        
        public ShipmentRepositoryMapper()
        {
            _invoiceRepositoryMapper = new InvoiceRepositoryMapper();
            _orderRepositoryMapper = new OrderRepositoryMapper();
            _shipmentItemRepositoryMapper = new ShipmentItemRepositoryMapper();
        }
        
        public DTO.Shipment Map(Shipment inObject)
        {
            return new DTO.Shipment()
            {
                Id = inObject.Id,
                Invoice = _invoiceRepositoryMapper.Map(inObject.Invoice!),
                InvoiceId = inObject.InvoiceId,
                Order = _orderRepositoryMapper.Map(inObject.Order!),
                OrderId = inObject.OrderId,
                ShipmentDate = inObject.ShipmentDate,
                ShipmentItems = inObject.ShipmentItems
                    .Select(e => _shipmentItemRepositoryMapper.Map(e)).ToList()
            };
        }

        public Shipment Map(DTO.Shipment inObject)
        {
            return new Shipment()
            {
                Id = inObject.Id,
                Invoice = _invoiceRepositoryMapper.Map(inObject.Invoice!),
                InvoiceId = inObject.InvoiceId,
                Order = _orderRepositoryMapper.Map(inObject.Order!),
                OrderId = inObject.OrderId,
                ShipmentDate = inObject.ShipmentDate,
                ShipmentItems = inObject.ShipmentItems
                    .Select(e => _shipmentItemRepositoryMapper.Map(e)).ToList()
            };        
        }
    }
}