using BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class CommentMapper : ApiBaseMapper<BLL.App.DTO.Comment, Comment>
    {
        public CommentView MapCommentView(BLLCommentView inObject)
        {
            return Mapper.Map<BLLCommentView, CommentView>(inObject);
        }

        public Comment MapComment(BLL.App.DTO.Comment inObject)
        {
            return new Comment()
            {
                AppUserId = inObject.AppUserId,
                CommentBody = inObject.CommentBody,
                CreatedAt = inObject.CreatedAt,
                CreatedBy = inObject.AppUser!.Email,
                Id = inObject.Id,
                PaintingId = inObject.PaintingId
            };
        }
        
    }
}