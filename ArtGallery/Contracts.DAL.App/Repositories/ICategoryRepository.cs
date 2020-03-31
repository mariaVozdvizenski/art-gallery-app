using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<bool> ExsistsAsync(Guid id, Guid? userId = null);
    }
}