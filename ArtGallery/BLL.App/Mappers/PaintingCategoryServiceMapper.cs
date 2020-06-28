using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using PaintingCategory = BLL.App.DTO.PaintingCategory;

namespace BLL.App.Mappers
{
    public class PaintingCategoryServiceMapper : AppServiceBaseMapper<DAL.App.DTO.PaintingCategory, PaintingCategory>, 
        IPaintingCategoryServiceMapper
    {
        
    }
}