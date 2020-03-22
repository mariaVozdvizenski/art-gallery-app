using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork: EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork

    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IArtistRepository Artists => GetRepository<IArtistRepository>(() => new ArtistRepository(UOWDbContext));
        public IBasketRepository Baskets => GetRepository<IBasketRepository>(() => new BasketRepository(UOWDbContext));
        public IBasketItemRepository BasketItems => GetRepository<IBasketItemRepository>(() => new BasketItemRepository(UOWDbContext));
        public ICategoryRepository Categories => GetRepository<ICategoryRepository>(() => new CategoryRepository(UOWDbContext));
        public ICommentRepository Comments => GetRepository<ICommentRepository>(() => new CommentRepository(UOWDbContext));
        public IInvoiceRepository Invoices => GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));
        public IInvoiceStatusCodeRepository InvoiceStatusCodes => GetRepository<IInvoiceStatusCodeRepository>(() => new InvoiceStatusCodeRepository(UOWDbContext));
        public IOrderRepository Orders => GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));
        public IOrderItemRepository OrderItems => GetRepository<IOrderItemRepository>(() => new OrderItemRepository(UOWDbContext));
        public IOrderStatusCodeRepository OrderStatusCodes => GetRepository<IOrderStatusCodeRepository>(() => new OrderStatusCodeRepository(UOWDbContext));
        public IPaintingRepository Paintings => GetRepository<IPaintingRepository>(() => new PaintingRepository(UOWDbContext));
        public IPaintingCategoryRepository PaintingCategories => GetRepository<IPaintingCategoryRepository>(() => new PaintingCategoryRepository(UOWDbContext));
        public IPaymentRepository PaymentsRepository => GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));
        public IPaymentMethodRepository PaymentMethods => GetRepository<IPaymentMethodRepository>(() => new PaymentMethodRepository(UOWDbContext));
        public IShipmentRepository Shipments => GetRepository<IShipmentRepository>(() => new ShipmentRepository(UOWDbContext));
        public IShipmentItemRepository ShipmentItems => GetRepository<IShipmentItemRepository>(() => new ShipmentItemRepository(UOWDbContext));
        public IUserPaymentMethodRepository UserPaymentMethods => GetRepository<IUserPaymentMethodRepository>(() => new UserPaymentMethodRepository(UOWDbContext));
    }
}