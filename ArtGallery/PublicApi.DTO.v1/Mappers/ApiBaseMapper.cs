using AutoMapper;
using BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class ApiBaseMapper<TLeft, TRight> : BaseMapper<TLeft, TRight>
        where TLeft : class?, new()
        where TRight : class?, new()
    {
        public ApiBaseMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLPaintingView, PaintingView>();
            MapperConfigurationExpression.CreateMap<BLLCommentView, CommentView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Artist, ArtistView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Comment, Comment>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Painting, Painting>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Basket, Basket>();

            
            MapperConfigurationExpression.CreateMap<PaintingView, BLLPaintingView>();
            MapperConfigurationExpression.CreateMap<CommentView, BLLCommentView>();
            MapperConfigurationExpression.CreateMap<ArtistView, BLL.App.DTO.Artist>();
            MapperConfigurationExpression.CreateMap<Comment, BLL.App.DTO.Comment>();
            MapperConfigurationExpression.CreateMap<Painting, BLL.App.DTO.Painting>();
            MapperConfigurationExpression.CreateMap<Basket, BLL.App.DTO.Basket>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}