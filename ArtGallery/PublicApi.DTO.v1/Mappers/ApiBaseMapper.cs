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
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.BasketItem, BasketItem>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PaintingCategory, PaintingCategory>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PaymentMethod, PaymentMethod>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderStatusCode, OrderStatusCode>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Address, Address>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Order, Order>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderItem, OrderItem>();

            MapperConfigurationExpression.CreateMap<PaintingView, BLLPaintingView>();
            MapperConfigurationExpression.CreateMap<CommentView, BLLCommentView>();
            MapperConfigurationExpression.CreateMap<ArtistView, BLL.App.DTO.Artist>();
            MapperConfigurationExpression.CreateMap<Comment, BLL.App.DTO.Comment>();
            MapperConfigurationExpression.CreateMap<Painting, BLL.App.DTO.Painting>();
            MapperConfigurationExpression.CreateMap<Basket, BLL.App.DTO.Basket>();
            MapperConfigurationExpression.CreateMap<BasketItem, BLL.App.DTO.BasketItem>();
            MapperConfigurationExpression.CreateMap<PaintingCategory, BLL.App.DTO.PaintingCategory>();
            MapperConfigurationExpression.CreateMap<PaymentMethod, BLL.App.DTO.PaymentMethod>();
            MapperConfigurationExpression.CreateMap<OrderStatusCode, BLL.App.DTO.OrderStatusCode>();
            MapperConfigurationExpression.CreateMap<InvoiceStatusCode, BLL.App.DTO.InvoiceStatusCode>();
            MapperConfigurationExpression.CreateMap<Address, BLL.App.DTO.Address>();
            MapperConfigurationExpression.CreateMap<Order, BLL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<OrderItem, BLL.App.DTO.OrderItem>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}