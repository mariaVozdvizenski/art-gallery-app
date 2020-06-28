using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class CategoryService : BaseEntityService<IAppUnitOfWork, ICategoryRepository, ICategoryServiceMapper, Category, DTO.Category>, 
        ICategoryService
    {
        public CategoryService(IAppUnitOfWork uow) : base(uow, uow.Categories, new CategoryServiceMapper())
        {
        }
    }
}