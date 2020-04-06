using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceRepository: IBaseRepository<Invoice>
    {
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<Invoice>> AllAsync(Guid? userId = null);
        Task<Invoice> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}