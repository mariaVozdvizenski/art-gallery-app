using System.Linq;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderMapper : ApiBaseMapper<BLL.App.DTO.Order, Order>
    {
        private readonly AddressMapper _addressMapper = new AddressMapper();
        private readonly OrderItemMapper _orderItemMapper = new OrderItemMapper();
        public AdminOrderView MapForAdminViewAsync(BLL.App.DTO.Order inObject)
        {
            return new AdminOrderView()
            {
                AddressId = inObject.AddressId,
                AppUserId = inObject.AppUserId,
                OrderStatusCode = inObject.OrderStatusCode!.Code,
                Id = inObject.Id,
                OrderDate = inObject.OrderDate,
                OrderDetails = inObject.OrderDetails,
                OrderStatusCodeId = inObject.OrderStatusCodeId,
                OrderStatusDescription = inObject.OrderStatusCode.OrderStatusDescription,
                UserName = inObject.AppUser!.UserName,
                Address = _addressMapper.Map(inObject.Address!),
                OrderItems = inObject.OrderItems
                    .Select(e => _orderItemMapper.MapForOrderItemView(e)).ToList()
            };
        }
    }
}