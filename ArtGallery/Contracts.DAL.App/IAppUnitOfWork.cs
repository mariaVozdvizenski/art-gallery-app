﻿using System;
using Contracts.DAL.App.Repositories;
using ee.itcollege.mavozd.Contracts.DAL.Base;using Domain.App.Identity;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
         IAddressRepository Addresses { get; }
         IArtistRepository Artists { get; }
         IBasketRepository Baskets { get; }
         IBasketItemRepository BasketItems { get; }
         ICategoryRepository Categories { get; }
         ICommentRepository Comments { get; }
         IInvoiceRepository Invoices { get; }
         IInvoiceStatusCodeRepository InvoiceStatusCodes { get; }
         IOrderRepository Orders { get; }
         IOrderItemRepository OrderItems { get; }
         IOrderStatusCodeRepository OrderStatusCodes { get; }
         IPaintingRepository Paintings { get; }
         IPaintingCategoryRepository PaintingCategories { get; }
         IPaymentRepository Payments { get; }
         IPaymentMethodRepository PaymentMethods { get; }
         IShipmentRepository Shipments { get; }
         IShipmentItemRepository ShipmentItems { get; }
         IUserPaymentMethodRepository UserPaymentMethods { get; }
    }
}