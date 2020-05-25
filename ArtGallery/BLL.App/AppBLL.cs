using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IArtistService Artists => 
            GetService<IArtistService>(() => new ArtistService(UOW));
        public IBasketItemService BasketItems => 
            GetService<IBasketItemService>(() => new BasketItemService(UOW));
        public IBasketService Baskets => 
            GetService<IBasketService>(() => new BasketService(UOW));
        public IPaintingService Paintings => 
            GetService<IPaintingService>(() => new PaintingService(UOW));

        public ICategoryService Categories => 
            GetService<ICategoryService>(() => new CategoryService(UOW));
        public ICommentService Comments => 
            GetService<ICommentService>(() => new CommentService(UOW));
        public IInvoiceService Invoices => 
            GetService<IInvoiceService>(() => new InvoiceService(UOW));
        public IInvoiceStatusCodeService InvoiceStatusCodes => 
            GetService<IInvoiceStatusCodeService>(() => new InvoiceStatusCodeService(UOW));
        public IOrderItemService OrderItems => 
            GetService<IOrderItemService>(() => new OrderItemService(UOW));
        public IOrderService Orders => 
            GetService<IOrderService>(() => new OrderService(UOW));
        public IOrderStatusCodeService OrderStatusCodes => 
            GetService<IOrderStatusCodeService>(() => new OrderStatusCodeService(UOW));
        public IPaintingCategoryService PaintingCategories => 
            GetService<IPaintingCategoryService>(() => new PaintingCategoryService(UOW));
        public IPaymentMethodService PaymentMethods => 
            GetService<IPaymentMethodService>(() => new PaymentMethodService(UOW));
        public IPaymentService Payments => 
            GetService<IPaymentService>(() => new PaymentService(UOW));
        public IShipmentService Shipments => 
            GetService<IShipmentService>(() => new ShipmentService(UOW));
        public IShipmentItemService ShipmentItems => 
            GetService<IShipmentItemService>(() => new ShipmentItemService(UOW));
        public IUserPaymentMethodService UserPaymentMethods => 
            GetService<IUserPaymentMethodService>(() => new UserPaymentMethodService(UOW));
    }
}