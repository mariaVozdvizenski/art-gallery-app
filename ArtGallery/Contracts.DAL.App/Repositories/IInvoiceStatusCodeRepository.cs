using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceStatusCodeRepository: IBaseRepository<InvoiceStatusCode>
    {
        Task<IEnumerable<InvoiceStatusCode>> AllAsync(Guid? userId = null);
        Task<InvoiceStatusCode> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
    }
}