using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderItemRepository: IBaseRepository<OrderItem>
    {
         Task<bool> ExistsAsync(Guid id, Guid? userId = null);
         Task<IEnumerable<OrderItem>> AllAsync(Guid? userId = null);
         Task<OrderItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null);

    }
}