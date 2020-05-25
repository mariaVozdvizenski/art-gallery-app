using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Invoice = BLL.App.DTO.Invoice;

namespace BLL.App.Mappers
{
    public class InvoiceServiceMapper :  IInvoiceServiceMapper
    {
        private readonly InvoiceStatusCodeMapper _invoiceStatusCodeMapper;
        private readonly OrderServiceMapper _orderServiceMapper;
        private readonly PaymentServiceMapper _paymentServiceMapper;
        private readonly ShipmentServiceMapper _shipmentServiceMapper;
        public InvoiceServiceMapper()
        {
            _invoiceStatusCodeMapper = new InvoiceStatusCodeMapper();
            _orderServiceMapper = new OrderServiceMapper();
            _paymentServiceMapper = new PaymentServiceMapper();
            _shipmentServiceMapper = new ShipmentServiceMapper();
        }
        public Invoice Map(DAL.App.DTO.Invoice inObject)
        {
            return new Invoice()
            {
                Id = inObject.Id,
                InvoiceDate = inObject.InvoiceDate,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCode = _invoiceStatusCodeMapper.Map(inObject.InvoiceStatusCode),
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Payments = inObject.Payments.Select(e => _paymentServiceMapper.Map(e)).ToList(),
                Shipments = inObject.Shipments.Select(e => _shipmentServiceMapper.Map(e)).ToList()
            };
        }

        public DAL.App.DTO.Invoice Map(Invoice inObject)
        {
            return new DAL.App.DTO.Invoice()
            {
                Id = inObject.Id,
                InvoiceDate = inObject.InvoiceDate,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCode = _invoiceStatusCodeMapper.Map(inObject.InvoiceStatusCode),
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                Order = _orderServiceMapper.Map(inObject.Order),
                OrderId = inObject.OrderId,
                Payments = inObject.Payments.Select(e => _paymentServiceMapper.Map(e)).ToList(),
                Shipments = inObject.Shipments.Select(e => _shipmentServiceMapper.Map(e)).ToList()
            };        
        }
    }
}