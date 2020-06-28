using System.Linq;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Category = BLL.App.DTO.Category;

namespace BLL.App.Mappers
{
    public class CategoryServiceMapper :  AppServiceBaseMapper<DAL.App.DTO.Category, Category>, ICategoryServiceMapper
    {
    }
}