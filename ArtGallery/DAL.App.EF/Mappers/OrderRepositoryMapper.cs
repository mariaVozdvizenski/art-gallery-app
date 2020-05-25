using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class OrderRepositoryMapper : IBaseMapper<Order, DTO.Order>
    {
        private readonly AppUserRepositoryMapper _appUserRepositoryMapper;
        private readonly InvoiceRepositoryMapper _invoiceRepositoryMapper;
        private readonly OrderItemRepositoryMapper _orderItemRepositoryMapper;
        private readonly OrderStatusCodeRepositoryMapper _orderStatusCodeRepositoryMapper;
        private readonly ShipmentRepositoryMapper _shipmentRepositoryMapper;
        
        public OrderRepositoryMapper()
        {
            _appUserRepositoryMapper = new AppUserRepositoryMapper();
            _invoiceRepositoryMapper = new InvoiceRepositoryMapper();
            _orderItemRepositoryMapper = new OrderItemRepositoryMapper();
            _orderStatusCodeRepositoryMapper = new OrderStatusCodeRepositoryMapper();
            _shipmentRepositoryMapper = new ShipmentRepositoryMapper();
        }
        
        public DTO.Order Map(Order inObject)
        {
            return new DTO.Order()
            {
                AppUser = _appUserRepositoryMapper.Map(inObject.AppUser),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceRepositoryMapper.Map(e)).ToList(),
                OrderDate = inObject.OrderDate,
                OrderDetails = inObject.OrderDetails,
                OrderItems = inObject.OrderItems.Select(e => _orderItemRepositoryMapper.Map(e)).ToList(),
                OrderStatusCode = _orderStatusCodeRepositoryMapper.Map(inObject.OrderStatusCode),
                OrderStatusCodeId = inObject.OrderStatusCodeId,
                Shipments = inObject.Shipments.Select(e => _shipmentRepositoryMapper.Map(e)).ToList()
            };
        }

        public Order Map(DTO.Order inObject)
        {
            return new Order()
            {
                AppUser = _appUserRepositoryMapper.Map(inObject.AppUser),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceRepositoryMapper.Map(e)).ToList(),
                OrderDate = inObject.OrderDate,
                OrderDetails = inObject.OrderDetails,
                OrderItems = inObject.OrderItems.Select(e => _orderItemRepositoryMapper.Map(e)).ToList(),
                OrderStatusCode = _orderStatusCodeRepositoryMapper.Map(inObject.OrderStatusCode),
                OrderStatusCodeId = inObject.OrderStatusCodeId,
                Shipments = inObject.Shipments.Select(e => _shipmentRepositoryMapper.Map(e)).ToList()
            };        
        }
    }
}