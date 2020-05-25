using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using InvoiceStatusCode = BLL.App.DTO.InvoiceStatusCode;

namespace BLL.App.Mappers
{
    public class InvoiceStatusCodeMapper :  IInvoiceStatusCodeMapper
    {
        private readonly InvoiceServiceMapper _invoiceServiceMapper;
        public InvoiceStatusCodeMapper()
        {
            _invoiceServiceMapper = new InvoiceServiceMapper();
        }
        public InvoiceStatusCode Map(DAL.App.DTO.InvoiceStatusCode inObject)
        {
            return new InvoiceStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceServiceMapper.Map(e)).ToList(),
                InvoiceStatusDescription = inObject.InvoiceStatusDescription
            };
        }

        public DAL.App.DTO.InvoiceStatusCode Map(InvoiceStatusCode inObject)
        {
            return new DAL.App.DTO.InvoiceStatusCode()
            {
                Code = inObject.Code,
                Id = inObject.Id,
                Invoices = inObject.Invoices.Select(e => _invoiceServiceMapper.Map(e)).ToList(),
                InvoiceStatusDescription = inObject.InvoiceStatusDescription
            };        
        }
    }
}