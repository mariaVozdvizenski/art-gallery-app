using System.Linq;

namespace PublicApi.DTO.v1.Mappers
{
    public class BasketMapper : ApiBaseMapper<BLL.App.DTO.Basket, Basket>
    {
        public BasketView MapBasketView(BLL.App.DTO.Basket inObject)
        {
            return new BasketView()
            {
                AppUserId = inObject.AppUserId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                UserName = inObject.AppUser!.UserName,
                BasketItems = inObject.BasketItems.Select(e => new BasketItemView()
                {
                    BasketId = e.BasketId,
                    DateCreated = e.DateCreated,
                    Id = e.Id,
                    PaintingId = e.PaintingId,
                    PaintingPrice = e.Painting!.Price,
                    PaintingQuantity = e.Painting.Quantity,
                    PaintingSize = e.Painting.Size,
                    PaintingTitle = e.Painting.Title,
                    Quantity = e.Quantity,
                    PaintingImageUrl = e.Painting.ImageName
                }).ToList()
            };
        }
    }
}