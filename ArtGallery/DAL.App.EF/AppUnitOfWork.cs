using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.mavozd.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork: EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork

    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IAddressRepository Addresses =>
            GetRepository<IAddressRepository>(() => new AddressRepository(UOWDbContext));
        public IArtistRepository Artists => 
            GetRepository<IArtistRepository>(() => new ArtistRepository(UOWDbContext));
        public IBasketRepository Baskets => 
            GetRepository<IBasketRepository>(() => new BasketRepository(UOWDbContext));
        public IBasketItemRepository BasketItems => 
            GetRepository<IBasketItemRepository>(() => new BasketItemRepository(UOWDbContext));
        public ICategoryRepository Categories => 
            GetRepository<ICategoryRepository>(() => new CategoryRepository(UOWDbContext));
        public ICommentRepository Comments => 
            GetRepository<ICommentRepository>(() => new CommentRepository(UOWDbContext));
        public IInvoiceRepository Invoices => 
            GetRepository<IInvoiceRepository>(() => new InvoiceRepository(UOWDbContext));
        public IInvoiceStatusCodeRepository InvoiceStatusCodes => 
            GetRepository<IInvoiceStatusCodeRepository>(() => new InvoiceStatusCodeRepository(UOWDbContext));
        public IOrderRepository Orders => 
            GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));
        public IOrderItemRepository OrderItems => 
            GetRepository<IOrderItemRepository>(() => new OrderItemRepository(UOWDbContext));
        public IOrderStatusCodeRepository OrderStatusCodes => 
            GetRepository<IOrderStatusCodeRepository>(() => new OrderStatusCodeRepository(UOWDbContext));
        public IPaintingRepository Paintings => 
            GetRepository<IPaintingRepository>(() => new PaintingRepository(UOWDbContext));
        public IPaintingCategoryRepository PaintingCategories => 
            GetRepository<IPaintingCategoryRepository>(() => new PaintingCategoryRepository(UOWDbContext));
        public IPaymentMethodRepository PaymentMethods => 
            GetRepository<IPaymentMethodRepository>(() => new PaymentMethodRepository(UOWDbContext));
        public IUserPaymentMethodRepository UserPaymentMethods => 
            GetRepository<IUserPaymentMethodRepository>(() => new UserPaymentMethodRepository(UOWDbContext));
    }
}