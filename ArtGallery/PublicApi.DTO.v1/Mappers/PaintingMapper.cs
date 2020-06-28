using System.Linq;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;

namespace PublicApi.DTO.v1.Mappers
{
    public class PaintingMapper : ApiBaseMapper<BLL.App.DTO.Painting, Painting>
    {
        private readonly CommentMapper _commentMapper = new CommentMapper();
        private readonly PaintingCategoryMapper _paintingCategoryMapper = new PaintingCategoryMapper();
        public PaintingView MapPaintingView(BLLPaintingView inObject)
        {
            return new PaintingView()
            {
                ArtistId = inObject.ArtistId,
                ArtistName = inObject.ArtistName,
                ImageName = inObject.ImageName,
                Comments = inObject.Comments.Select(e => _commentMapper.MapCommentView(e)).ToList(),
                Description = inObject.Description,
                Id = inObject.Id,
                PaintingCategories = inObject.PaintingCategories
                    .Select(e => _paintingCategoryMapper.MapPaintingCategoryView(e)).ToList(),
                Price = inObject.Price,
                Quantity = inObject.Quantity,
                Size = inObject.Size,
                Title = inObject.Title
            };
        }
    }
}