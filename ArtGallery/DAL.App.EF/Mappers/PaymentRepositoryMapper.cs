using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class PaymentRepositoryMapper : IBaseMapper<Payment, DTO.Payment>
    {
        private readonly InvoiceRepositoryMapper _invoiceRepositoryMapper;

        public PaymentRepositoryMapper()
        {
            _invoiceRepositoryMapper = new InvoiceRepositoryMapper();
        }
        public DTO.Payment Map(Payment inObject)
        {
            return new DTO.Payment()
            {
                Id = inObject.Id,
                Invoice = _invoiceRepositoryMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                PaymentAmount = inObject.PaymentAmount,
                PaymentDate = inObject.PaymentDate
            };
        }

        public Payment Map(DTO.Payment inObject)
        {
            return new Payment()
            {
                Id = inObject.Id,
                Invoice = _invoiceRepositoryMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                PaymentAmount = inObject.PaymentAmount,
                PaymentDate = inObject.PaymentDate
            };        
        }
    }
}