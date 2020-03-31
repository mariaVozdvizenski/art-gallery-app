using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaintingCategoryRepository: IBaseRepository<PaintingCategory>
    {
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
        Task<IEnumerable<PaintingCategory>> AllAsync(Guid? userId = null);
        Task<PaintingCategory> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
    }
}