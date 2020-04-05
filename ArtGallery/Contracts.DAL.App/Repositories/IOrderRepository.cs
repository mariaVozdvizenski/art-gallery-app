using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository: IBaseRepository<Order>
    {
        Task<Order> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
        Task<IEnumerable<Order>> AllAsync(Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}