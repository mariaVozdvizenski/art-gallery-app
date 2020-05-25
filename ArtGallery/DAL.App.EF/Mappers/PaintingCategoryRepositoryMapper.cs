using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class PaintingCategoryRepositoryMapper : IBaseMapper<PaintingCategory, DTO.PaintingCategory>
    {
        private readonly CategoryRepositoryMapper _categoryRepositoryMapper;
        private readonly PaintingRepositoryMapper _paintingRepositoryMapper;
        
        public PaintingCategoryRepositoryMapper()
        {
            _categoryRepositoryMapper = new CategoryRepositoryMapper();
            _paintingRepositoryMapper = new PaintingRepositoryMapper();
        }
        public DTO.PaintingCategory Map(PaintingCategory inObject)
        {
            return new DTO.PaintingCategory()
            {
                Category = _categoryRepositoryMapper.Map(inObject.Category),
                CategoryId = inObject.CategoryId,
                Id = inObject.Id,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };
        }

        public PaintingCategory Map(DTO.PaintingCategory inObject)
        {
            return new PaintingCategory()
            {
                Category = _categoryRepositoryMapper.Map(inObject.Category),
                CategoryId = inObject.CategoryId,
                Id = inObject.Id,
                Painting = _paintingRepositoryMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };
        }
    }
}