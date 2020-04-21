using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Microsoft.EntityFrameworkCore;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketItemRepository : IBaseRepository<BasketItem>
    {
         Task<bool> ExsistsAsync(Guid id, Guid? userId = null);
         Task<IEnumerable<BasketItem>> AllAsync(Guid? userId = null);
         Task<BasketItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
         Task DeleteAsync(Guid id, Guid? userId = null);
    }
}