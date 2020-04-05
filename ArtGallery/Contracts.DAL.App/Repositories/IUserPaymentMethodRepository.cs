using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserPaymentMethodRepository: IBaseRepository<UserPaymentMethod>
    {
        Task<IEnumerable<UserPaymentMethod>> AllAsync(Guid? userId = null);
        Task<UserPaymentMethod> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}