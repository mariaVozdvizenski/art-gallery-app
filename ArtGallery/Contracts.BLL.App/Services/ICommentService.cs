using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface ICommentService : IBaseEntityService<Comment>, ICommentRepositoryCustom<BLLCommentView>
    {
    }
}