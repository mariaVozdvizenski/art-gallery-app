using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Payment = BLL.App.DTO.Payment;

namespace BLL.App.Mappers
{
    public class PaymentServiceMapper :  IPaymentServiceMapper
    {
        private readonly InvoiceServiceMapper _invoiceServiceMapper;

        public PaymentServiceMapper()
        {
            _invoiceServiceMapper = new InvoiceServiceMapper();
        }
        public Payment Map(DAL.App.DTO.Payment inObject)
        {
            return new Payment()
            {
                Id = inObject.Id,
                Invoice = _invoiceServiceMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                PaymentAmount = inObject.PaymentAmount,
                PaymentDate = inObject.PaymentDate
            };
        }

        public DAL.App.DTO.Payment Map(Payment inObject)
        {
            return new DAL.App.DTO.Payment()
            {
                Id = inObject.Id,
                Invoice = _invoiceServiceMapper.Map(inObject.Invoice),
                InvoiceId = inObject.InvoiceId,
                PaymentAmount = inObject.PaymentAmount,
                PaymentDate = inObject.PaymentDate
            };        
        }
    }
}