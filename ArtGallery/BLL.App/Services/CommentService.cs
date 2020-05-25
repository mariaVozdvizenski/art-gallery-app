using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Comment = DAL.App.DTO.Comment;

namespace BLL.App.Services
{
    public class CommentService : BaseEntityService<IAppUnitOfWork, ICommentRepository, ICommentServiceMapper, Comment, DTO.Comment>,
        ICommentService
    {
        public CommentService(IAppUnitOfWork uow) : base(uow, uow.Comments, new CommentServiceMapper())
        {
        }
        
        public async Task<IEnumerable<BLLCommentView>> GetAllForViewAsync()
        {
            var dalCommentViews = await Repository.GetAllForViewAsync();
            var bllCommentViews = dalCommentViews.Select(e => Mapper.MapCommentView(e));
            return bllCommentViews;
        }
    }
}