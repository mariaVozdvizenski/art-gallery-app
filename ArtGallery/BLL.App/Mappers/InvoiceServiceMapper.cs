using Contracts.BLL.App.Mappers;
using Invoice = BLL.App.DTO.Invoice;

namespace BLL.App.Mappers
{
    public class InvoiceServiceMapper : AppServiceBaseMapper<DAL.App.DTO.Invoice, Invoice>, IInvoiceServiceMapper
    {
    }
}