using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICommentRepository: IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> AllAsync(Guid? userId = null);
        Task<Comment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        //Task<IEnumerable<CommentDTO>> DTOAllAsync(Guid? userId = null);
        //Task<CommentDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}