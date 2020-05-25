using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class CategoryRepositoryMapper : IBaseMapper<Category, DTO.Category>
    {
        private readonly PaintingCategoryRepositoryMapper _paintingCategoryRepositoryMapper;
        public CategoryRepositoryMapper()
        {
            _paintingCategoryRepositoryMapper = new PaintingCategoryRepositoryMapper();
        }
        public DTO.Category Map(Category inObject)
        {
            return new DTO.Category()
            {
                CategoryName = inObject.CategoryName,
                Id = inObject.Id,
                CategoryPaintings = inObject.CategoryPaintings
                    .Select(e => _paintingCategoryRepositoryMapper.Map(e)).ToList()
            };
        }

        public Category Map(DTO.Category inObject)
        {
            return new Category()
            {
                CategoryName = inObject.CategoryName,
                Id = inObject.Id,
                CategoryPaintings = inObject.CategoryPaintings
                    .Select(e => _paintingCategoryRepositoryMapper.Map(e)).ToList()
            };        
        }
    }
}