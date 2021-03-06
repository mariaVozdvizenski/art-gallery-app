﻿using System;
using Contracts.BLL.App.Services;
using ee.itcollege.mavozd.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAddressService Addresses { get;  }
        IArtistService Artists { get; }
        IBasketItemService BasketItems { get; }
        IBasketService Baskets { get; }
        IPaintingService Paintings { get; }
        ICategoryService Categories { get; }
        ICommentService Comments { get; }
        IInvoiceService Invoices { get; }
        IInvoiceStatusCodeService InvoiceStatusCodes { get; }
        IOrderItemService OrderItems { get; }
        IOrderService Orders { get; }
        IOrderStatusCodeService OrderStatusCodes { get; }
        IPaintingCategoryService PaintingCategories { get; }
        IPaymentMethodService PaymentMethods { get; }
        IUserPaymentMethodService UserPaymentMethods { get; }
    }
}