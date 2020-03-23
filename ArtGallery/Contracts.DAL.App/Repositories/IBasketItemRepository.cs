using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Contracts.DAL.Base.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketItemRepository : IBaseRepository<BasketItem>
    {
        /*IEnumerable<BasketItem> Include(Expression<Func<BasketItem, object>> criteria);*/
    }
}