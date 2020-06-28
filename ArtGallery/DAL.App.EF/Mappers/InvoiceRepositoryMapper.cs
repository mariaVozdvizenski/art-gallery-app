using System.Linq;
using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class InvoiceRepositoryMapper : IBaseMapper<Invoice, DTO.Invoice>
    {
        private readonly InvoiceStatusCodeRepositoryMapper _invoiceStatusCodeRepositoryMapper;
        private readonly OrderRepositoryMapper _orderRepositoryMapper;
        private readonly PaymentRepositoryMapper _paymentRepositoryMapper;
        private readonly ShipmentRepositoryMapper _shipmentRepositoryMapper;
        public InvoiceRepositoryMapper()
        {
            _invoiceStatusCodeRepositoryMapper = new InvoiceStatusCodeRepositoryMapper();
            _orderRepositoryMapper = new OrderRepositoryMapper();
            _paymentRepositoryMapper = new PaymentRepositoryMapper();
            _shipmentRepositoryMapper = new ShipmentRepositoryMapper();
        }
        public DTO.Invoice Map(Invoice inObject)
        {
            return new DTO.Invoice()
            {
                Id = inObject.Id,
                InvoiceDate = inObject.InvoiceDate,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCode = _invoiceStatusCodeRepositoryMapper.Map(inObject.InvoiceStatusCode!),
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                Order = _orderRepositoryMapper.Map(inObject.Order!),
                OrderId = inObject.OrderId,
                Payments = inObject.Payments.Select(e => _paymentRepositoryMapper.Map(e)).ToList(),
                Shipments = inObject.Shipments.Select(e => _shipmentRepositoryMapper.Map(e)).ToList()
            };
        }

        public Invoice Map(DTO.Invoice inObject)
        {
            return new Invoice()
            {
                Id = inObject.Id,
                InvoiceDate = inObject.InvoiceDate,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCode = _invoiceStatusCodeRepositoryMapper.Map(inObject.InvoiceStatusCode!),
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                Order = _orderRepositoryMapper.Map(inObject.Order!),
                OrderId = inObject.OrderId,
                Payments = inObject.Payments.Select(e => _paymentRepositoryMapper.Map(e)).ToList(),
                Shipments = inObject.Shipments.Select(e => _shipmentRepositoryMapper.Map(e)).ToList()
            };        
        }
    }
}