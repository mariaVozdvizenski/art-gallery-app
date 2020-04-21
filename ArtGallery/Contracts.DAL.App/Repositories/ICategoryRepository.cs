using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<Category>> AllAsync(Guid? userId = null);
        Task<Category> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        //Task<IEnumerable<CategoryDTO>> DTOAllAsync(Guid? userId = null);
        //Task<CategoryDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}