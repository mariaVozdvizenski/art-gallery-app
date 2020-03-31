using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderStatusCodeRepository: IBaseRepository<OrderStatusCode>
    {
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
    }
}