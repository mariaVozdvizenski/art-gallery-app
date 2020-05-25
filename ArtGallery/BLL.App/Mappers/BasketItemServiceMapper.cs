using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BasketItem = BLL.App.DTO.BasketItem;

namespace BLL.App.Mappers
{
    public class BasketItemServiceMapper :  IBasketItemServiceMapper
    {
        private readonly BasketServiceMapper _basketServiceMapper;
        private readonly PaintingServiceMapper _paintingServiceMapper;
        public BasketItemServiceMapper()
        {
            _basketServiceMapper = new BasketServiceMapper();
            _paintingServiceMapper = new PaintingServiceMapper();
        }
        public BasketItem Map(DAL.App.DTO.BasketItem inObject)
        {
            return new BasketItem()
            {
                Basket = _basketServiceMapper.Map(inObject.Basket),
                BasketId = inObject.BasketId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId,
                Quantity = inObject.Quantity
            };
        }

        public DAL.App.DTO.BasketItem Map(BasketItem inObject)
        {
            return new DAL.App.DTO.BasketItem()
            {
                Basket = _basketServiceMapper.Map(inObject.Basket),
                BasketId = inObject.BasketId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId,
                Quantity = inObject.Quantity
            };        
        }
    }
}