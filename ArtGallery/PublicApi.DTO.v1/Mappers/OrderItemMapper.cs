namespace PublicApi.DTO.v1.Mappers
{
    public class OrderItemMapper : ApiBaseMapper<BLL.App.DTO.OrderItem, OrderItem>
    {
        public OrderItemView MapForOrderItemView(BLL.App.DTO.OrderItem inObject)
        {
            return new OrderItemView()
            {
                Id = inObject.Id,
                OrderId = inObject.OrderId,
                PaintingId = inObject.PaintingId,
                PaintingPrice = inObject.Painting!.Price,
                ImageName = inObject.Painting!.ImageName,
                PaintingTitle = inObject.Painting.Title,
                Quantity = inObject.Quantity
            };
        }
    }
}