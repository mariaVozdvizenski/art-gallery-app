using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaintingRepository: IBaseRepository<Painting>
    {
        Task<bool> ExistsAsync (Guid? id, Guid? userId = null);
        Task<IEnumerable<Painting>> AllAsync (Guid? userId = null);
        Task<Painting> FirstOrDefaultAsync(Guid? id, Guid? userId = null);

    }
}