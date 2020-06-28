using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class InvoiceStatusCodeService : BaseEntityService<IAppUnitOfWork, IInvoiceStatusCodeRepository, IInvoiceStatusCodeMapper, 
        InvoiceStatusCode, DTO.InvoiceStatusCode>, IInvoiceStatusCodeService
    {
        public InvoiceStatusCodeService(IAppUnitOfWork uow) : base(uow, uow.InvoiceStatusCodes, new InvoiceStatusCodeServiceMapper())
        {
        }
    }
}