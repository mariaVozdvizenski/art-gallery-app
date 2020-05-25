using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using Comment = DAL.App.DTO.Comment;

namespace Contracts.DAL.App.Repositories
{
    public interface ICommentRepository: IBaseRepository<Comment>, ICommentRepositoryCustom
    {
    }
}