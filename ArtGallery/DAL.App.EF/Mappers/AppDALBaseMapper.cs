using AutoMapper;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF.Mappers
{
    public class AppDALBaseMapper<TLeft, TRight> : BaseMapper<TLeft, TRight> 
        where TLeft : class, new() 
        where TRight : class, new()
    {
        public AppDALBaseMapper()
        {
            MapperConfigurationExpression.CreateMap<Artist, DTO.Artist>();
            MapperConfigurationExpression.CreateMap<BasketItem, DTO.BasketItem>();
            MapperConfigurationExpression.CreateMap<Basket, DTO.Basket>();
            MapperConfigurationExpression.CreateMap<Category, DTO.Category>();
            MapperConfigurationExpression.CreateMap<Comment, DTO.Comment>();
            MapperConfigurationExpression.CreateMap<Invoice, DTO.Invoice>();
            MapperConfigurationExpression.CreateMap<InvoiceStatusCode, DTO.InvoiceStatusCode>();
            MapperConfigurationExpression.CreateMap<OrderItem, DTO.OrderItem>();
            MapperConfigurationExpression.CreateMap<Order, DTO.Order>();
            MapperConfigurationExpression.CreateMap<OrderStatusCode, DTO.OrderStatusCode>();
            MapperConfigurationExpression.CreateMap<PaintingCategory, DTO.PaintingCategory>();
            MapperConfigurationExpression.CreateMap<Painting, DTO.Painting>();
            MapperConfigurationExpression.CreateMap<PaymentMethod, DTO.PaymentMethod>();
            MapperConfigurationExpression.CreateMap<Payment, DTO.Payment>();
            MapperConfigurationExpression.CreateMap<ShipmentItem, DTO.ShipmentItem>();
            MapperConfigurationExpression.CreateMap<Shipment, DTO.Shipment>();
            MapperConfigurationExpression.CreateMap<UserPaymentMethod, DTO.UserPaymentMethod>();
            MapperConfigurationExpression.CreateMap<AppUser, DTO.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<DTO.Artist, Artist>();
            MapperConfigurationExpression.CreateMap<DTO.BasketItem, BasketItem>();
            MapperConfigurationExpression.CreateMap<DTO.Basket, Basket>();
            MapperConfigurationExpression.CreateMap<DTO.Category, Category>();
            MapperConfigurationExpression.CreateMap<DTO.Comment, Comment>();
            MapperConfigurationExpression.CreateMap<DTO.Invoice, Invoice>();
            MapperConfigurationExpression.CreateMap<DTO.InvoiceStatusCode, InvoiceStatusCode>();
            MapperConfigurationExpression.CreateMap<DTO.OrderItem, OrderItem>();
            MapperConfigurationExpression.CreateMap<DTO.Order, Order>();
            MapperConfigurationExpression.CreateMap<DTO.OrderStatusCode, OrderStatusCode>();
            MapperConfigurationExpression.CreateMap<DTO.PaintingCategory, PaintingCategory>();
            MapperConfigurationExpression.CreateMap<DTO.Painting, Painting>();
            MapperConfigurationExpression.CreateMap<DTO.PaymentMethod, PaymentMethod>();
            MapperConfigurationExpression.CreateMap<DTO.Payment, Payment>();
            MapperConfigurationExpression.CreateMap<DTO.ShipmentItem, ShipmentItem>();
            MapperConfigurationExpression.CreateMap<DTO.Shipment, Shipment>();
            MapperConfigurationExpression.CreateMap<DTO.UserPaymentMethod, UserPaymentMethod>();
            MapperConfigurationExpression.CreateMap<DTO.Identity.AppUser, AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}