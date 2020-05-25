using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using PaintingCategory = BLL.App.DTO.PaintingCategory;

namespace BLL.App.Mappers
{
    public class PaintingCategoryServiceMapper : IPaintingCategoryServiceMapper
    {
        private readonly CategoryServiceMapper _categoryServiceMapper;
        private readonly PaintingServiceMapper _paintingServiceMapper;
        public PaintingCategoryServiceMapper()
        {
            _paintingServiceMapper = new PaintingServiceMapper();
            _categoryServiceMapper = new CategoryServiceMapper();
        }
        public PaintingCategory Map(DAL.App.DTO.PaintingCategory inObject)
        {
            return new PaintingCategory()
            {
                Category = _categoryServiceMapper.Map(inObject.Category),
                CategoryId = inObject.CategoryId,
                Id = inObject.Id,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };
        }

        public DAL.App.DTO.PaintingCategory Map(PaintingCategory inObject)
        {
            return new DAL.App.DTO.PaintingCategory()
            {
                Category = _categoryServiceMapper.Map(inObject.Category),
                CategoryId = inObject.CategoryId,
                Id = inObject.Id,
                Painting = _paintingServiceMapper.Map(inObject.Painting),
                PaintingId = inObject.PaintingId
            };        
        }
    }
}