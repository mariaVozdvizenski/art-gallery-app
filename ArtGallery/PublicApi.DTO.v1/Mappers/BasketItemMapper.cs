namespace PublicApi.DTO.v1.Mappers
{
    public class BasketItemMapper : ApiBaseMapper<BLL.App.DTO.BasketItem, BasketItem>
    {
        public BasketItemView MapForViewAsync(BLL.App.DTO.BasketItem inObject)
        {
            return new BasketItemView()
            {
                BasketId = inObject.BasketId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                PaintingId = inObject.PaintingId,
                PaintingPrice = inObject.Painting!.Price,
                PaintingQuantity = inObject.Painting.Quantity,
                PaintingSize = inObject.Painting.Size,
                PaintingTitle = inObject.Painting.Title,
                Quantity = inObject.Quantity
            };
        }
    }
}