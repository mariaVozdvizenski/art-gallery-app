using System.Linq;
using Contracts.BLL.App.Mappers;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using InvoiceStatusCode = BLL.App.DTO.InvoiceStatusCode;

namespace BLL.App.Mappers
{
    public class InvoiceStatusCodeServiceMapper : AppServiceBaseMapper<DAL.App.DTO.InvoiceStatusCode, InvoiceStatusCode>,
        IInvoiceStatusCodeMapper
    {
    }
}