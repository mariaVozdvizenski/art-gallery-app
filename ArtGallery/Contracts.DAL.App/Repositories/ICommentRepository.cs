using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface ICommentRepository: IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> AllAsync(Guid? userId = null);
        Task<Comment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}