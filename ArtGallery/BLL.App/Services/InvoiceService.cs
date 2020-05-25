using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class InvoiceService : BaseEntityService<IAppUnitOfWork, IInvoiceRepository, IInvoiceServiceMapper, Invoice, DTO.Invoice>,
        IInvoiceService
    {
        public InvoiceService(IAppUnitOfWork uow) : base(uow, uow.Invoices, new InvoiceServiceMapper())
        {
        }
    }
}