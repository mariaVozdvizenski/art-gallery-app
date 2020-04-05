using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<Category>> AllAsync(Guid? userId = null);
        Task<Category> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
    }
}