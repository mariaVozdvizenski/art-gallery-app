using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository: IBaseRepository<Payment>
    {
        public Task<Payment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
        Task<IEnumerable<Payment>> AllAsync(Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}