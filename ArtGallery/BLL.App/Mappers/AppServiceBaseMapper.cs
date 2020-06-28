using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using Address = DAL.App.DTO.Address;
using Artist = DAL.App.DTO.Artist;
using Basket = DAL.App.DTO.Basket;
using BasketItem = DAL.App.DTO.BasketItem;
using Category = DAL.App.DTO.Category;
using Comment = DAL.App.DTO.Comment;
using Invoice = DAL.App.DTO.Invoice;
using InvoiceStatusCode = DAL.App.DTO.InvoiceStatusCode;
using Order = DAL.App.DTO.Order;
using OrderItem = DAL.App.DTO.OrderItem;
using OrderStatusCode = DAL.App.DTO.OrderStatusCode;
using Painting = DAL.App.DTO.Painting;
using PaintingCategory = DAL.App.DTO.PaintingCategory;
using Payment = DAL.App.DTO.Payment;
using PaymentMethod = DAL.App.DTO.PaymentMethod;
using Shipment = DAL.App.DTO.Shipment;
using ShipmentItem = DAL.App.DTO.ShipmentItem;
using UserPaymentMethod = DAL.App.DTO.UserPaymentMethod;

namespace BLL.App.Mappers
{
    public class AppServiceBaseMapper<TLeft, TRight> : BaseMapper<TLeft, TRight> 
        where TLeft : class, new() 
        where TRight : class, new()
    {
        
        public AppServiceBaseMapper()
        {
            MapperConfigurationExpression.CreateMap<Address, DTO.Address>();
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
            MapperConfigurationExpression.CreateMap<DALPaintingView, BLLPaintingView>();
            MapperConfigurationExpression.CreateMap<DALCommentView, BLLCommentView>();

            MapperConfigurationExpression.CreateMap<DTO.Address, Address>();
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
            MapperConfigurationExpression.CreateMap<BLLPaintingView, DALPaintingView>();
            MapperConfigurationExpression.CreateMap<BLLCommentView, DALCommentView>();
            
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}