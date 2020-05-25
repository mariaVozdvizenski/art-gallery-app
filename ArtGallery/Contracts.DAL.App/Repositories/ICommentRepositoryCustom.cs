using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICommentRepositoryCustom : ICommentRepositoryCustom<DALCommentView>
    {
    }

    public interface ICommentRepositoryCustom<TCommentView>
    {
        Task<IEnumerable<TCommentView>> GetAllForViewAsync();
    }
}