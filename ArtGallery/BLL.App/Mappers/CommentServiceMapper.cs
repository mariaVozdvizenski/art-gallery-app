using BLL.App.DTO;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Comment = DAL.App.DTO.Comment;

namespace BLL.App.Mappers
{
    public class CommentServiceMapper : AppServiceBaseMapper<Comment, BLL.App.DTO.Comment>, ICommentServiceMapper
    {
        
        public BLLCommentView MapCommentView(DALCommentView inObject)
        {
            return Mapper.Map<DALCommentView, BLLCommentView>(inObject);
        }
    }
}