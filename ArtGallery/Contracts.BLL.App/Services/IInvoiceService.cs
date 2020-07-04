using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;
using PublicApi.DTO.v1;
using Invoice = BLL.App.DTO.Invoice;

namespace Contracts.BLL.App.Services
{
    public interface IInvoiceService : IBaseEntityService<Invoice>
    {
        Task CalculateInvoiceDataAsync(Invoice invoice);
        Task GenerateInvoiceAsync(Guid invoiceId, InvoiceCreate extraData);
        Task<IEnumerable<Invoice>> GetInvoicesForCurrentOrderAsync(Guid orderId);
    }
}