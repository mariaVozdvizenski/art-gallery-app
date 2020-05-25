using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class BasketItemRepositoryMapper : IBaseMapper<BasketItem, DTO.BasketItem>
    {
        private readonly BasketRepositoryMapper _basketRepositoryMapper;
        private readonly PaintingRepositoryMapper _paintingRepositoryMapper;
        public BasketItemRepositoryMapper()
        {
            _basketRepositoryMapper = new BasketRepositoryMapper();   
            _paintingRepositoryMapper = new PaintingRepositoryMapper();
        }
        
        public DTO.BasketItem Map(BasketItem inObject)
        {
            return new DTO.BasketItem()
            {
                Basket = _basketRepositoryMapper.Map(inObject.Basket),
                BasketId = inObject.BasketId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId,
                Quantity = inObject.Quantity
            };
        }

        public BasketItem Map(DTO.BasketItem inObject)
        {
            return new BasketItem()
            {
                Basket = _basketRepositoryMapper.Map(inObject.Basket),
                BasketId = inObject.BasketId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId,
                Quantity = inObject.Quantity
            };
            
        }
    }
}