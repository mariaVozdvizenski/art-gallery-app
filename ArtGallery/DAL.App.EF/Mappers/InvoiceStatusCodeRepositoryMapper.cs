using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class InvoiceStatusCodeRepositoryMapper : IBaseMapper<InvoiceStatusCode, DTO.InvoiceStatusCode>
    {
        private readonly InvoiceRepositoryMapper _invoiceRepositoryMapper;
        
        public InvoiceStatusCodeRepositoryMapper()
        {
            _invoiceRepositoryMapper = new InvoiceRepositoryMapper();
        }
        public DTO.InvoiceStatusCode Map(InvoiceStatusCode inObject)
        {
            return new DTO.InvoiceStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceRepositoryMapper.Map(e)).ToList(),
                InvoiceStatusDescription = inObject.InvoiceStatusDescription
            };
        }

        public InvoiceStatusCode Map(DTO.InvoiceStatusCode inObject)
        {
            return new InvoiceStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceRepositoryMapper.Map(e)).ToList(),
                InvoiceStatusDescription = inObject.InvoiceStatusDescription
            };        
        }
    }
}