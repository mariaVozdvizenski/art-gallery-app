using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentMethodRepository: IBaseRepository<PaymentMethod>
    {
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
    }
}