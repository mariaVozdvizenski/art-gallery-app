using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository: IBaseRepository<Payment>
    {
        Task<bool> ExistsAsync (Guid? id, Guid? userId = null);
        Task<IEnumerable<Payment>> AllAsync (Guid? userId = null);
        Task<Payment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
    }
}