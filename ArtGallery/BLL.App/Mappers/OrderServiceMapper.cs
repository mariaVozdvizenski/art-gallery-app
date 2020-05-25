using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Order = BLL.App.DTO.Order;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper : IOrderServiceMapper
    {
        private readonly AppUserServiceMapper _appUserServiceMapper;
        private readonly InvoiceServiceMapper _invoiceServiceMapper;
        private readonly OrderItemServiceMapper _orderItemServiceMapper;
        private readonly OrderStatusCodeServiceMapper _orderStatusCodeServiceMapper;
        private readonly ShipmentServiceMapper _shipmentServiceMapper;
        public OrderServiceMapper()
        {
            _appUserServiceMapper = new AppUserServiceMapper();
            _invoiceServiceMapper = new InvoiceServiceMapper();
            _orderItemServiceMapper = new OrderItemServiceMapper();
            _orderStatusCodeServiceMapper = new OrderStatusCodeServiceMapper();
            _shipmentServiceMapper = new ShipmentServiceMapper();
        }
        public Order Map(DAL.App.DTO.Order inObject)
        {
            return new Order()
            {
                AppUser = _appUserServiceMapper.Map(inObject.AppUser),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceServiceMapper.Map(e)).ToList(),
                OrderDate = inObject.OrderDate,
                OrderDetails = inObject.OrderDetails,
                OrderItems = inObject.OrderItems.Select(e => _orderItemServiceMapper.Map(e)).ToList(),
                OrderStatusCode = _orderStatusCodeServiceMapper.Map(inObject.OrderStatusCode),
                OrderStatusCodeId = inObject.OrderStatusCodeId,
                Shipments = inObject.Shipments.Select(e => _shipmentServiceMapper.Map(e)).ToList()
            };
        }

        public DAL.App.DTO.Order Map(Order inObject)
        {
            return new DAL.App.DTO.Order()
            {
                AppUser = _appUserServiceMapper.Map(inObject.AppUser),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceServiceMapper.Map(e)).ToList(),
                OrderDate = inObject.OrderDate,
                OrderDetails = inObject.OrderDetails,
                OrderItems = inObject.OrderItems.Select(e => _orderItemServiceMapper.Map(e)).ToList(),
                OrderStatusCode = _orderStatusCodeServiceMapper.Map(inObject.OrderStatusCode),
                OrderStatusCodeId = inObject.OrderStatusCodeId,
                Shipments = inObject.Shipments.Select(e => _shipmentServiceMapper.Map(e)).ToList()
            };        
        }
    }
}