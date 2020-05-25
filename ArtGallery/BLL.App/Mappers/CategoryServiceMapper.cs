using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Category = BLL.App.DTO.Category;

namespace BLL.App.Mappers
{
    public class CategoryServiceMapper :  ICategoryServiceMapper
    {
        private readonly PaintingCategoryServiceMapper _paintingCategoryServiceMapper;
        
        public CategoryServiceMapper()
        {
            _paintingCategoryServiceMapper = new PaintingCategoryServiceMapper();
        }
        public Category Map(DAL.App.DTO.Category inObject)
        {
            return new Category()
            {
                CategoryName = inObject.CategoryName,
                CategoryPaintings = inObject.CategoryPaintings
                    .Select(e => _paintingCategoryServiceMapper.Map(e)).ToList(),
                Id = inObject.Id
            };
        }

        public DAL.App.DTO.Category Map(Category inObject)
        {
            return new DAL.App.DTO.Category()
            {
                CategoryName = inObject.CategoryName,
                CategoryPaintings = inObject.CategoryPaintings
                    .Select(e => _paintingCategoryServiceMapper.Map(e)).ToList(),
                Id = inObject.Id
            };        
        }
    }
}