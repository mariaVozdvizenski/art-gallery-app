using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Shipment = BLL.App.DTO.Shipment;

namespace BLL.App.Mappers
{
    public class ShipmentServiceMapper : IShipmentServiceMapper
    {
        private readonly InvoiceServiceMapper _invoiceServiceMapper;
        private readonly OrderServiceMapper _orderServiceMapper;
        private readonly ShipmentItemServiceMapper _shipmentItemServiceMapper;
        
        public ShipmentServiceMapper()
        {
            _invoiceServiceMapper = new InvoiceServiceMapper();
            _orderServiceMapper = new OrderServiceMapper();
            _shipmentItemServiceMapper = new ShipmentItemServiceMapper();
        }
        public Shipment Map(DAL.App.DTO.Shipment inObject)
        {
            return new Shipment()
            {
                Id = inObject.Id,
                Invoice = _invoiceServiceMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                ShipmentDate = inObject.ShipmentDate,
                ShipmentItems = inObject.ShipmentItems
                    .Select(e => _shipmentItemServiceMapper.Map(e)).ToList()
            };
        }

        public DAL.App.DTO.Shipment Map(Shipment inObject)
        {
            return new DAL.App.DTO.Shipment()
            {
                Id = inObject.Id,
                Invoice = _invoiceServiceMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                ShipmentDate = inObject.ShipmentDate,
                ShipmentItems = inObject.ShipmentItems
                    .Select(e => _shipmentItemServiceMapper.Map(e)).ToList()
            };        
        }
    }
}