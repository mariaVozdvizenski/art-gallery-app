using System.Linq;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using PaymentMethod = BLL.App.DTO.PaymentMethod;

namespace BLL.App.Mappers
{
    public class PaymentMethodServiceMapper : IPaymentMethodServiceMapper
    {
        private readonly UserPaymentMethodServiceMapper _userPaymentMethodServiceMapper;
        
        public PaymentMethodServiceMapper()
        {
            _userPaymentMethodServiceMapper = new UserPaymentMethodServiceMapper();
        }
        public PaymentMethod Map(DAL.App.DTO.PaymentMethod inObject)
        {
            return new PaymentMethod()
            {
                Id = inObject.Id,
                PaymentMethodCode = inObject.PaymentMethodCode,
                PaymentMethodDescription = inObject.PaymentMethodDescription,
                UserPaymentMethods = inObject.UserPaymentMethods
                    .Select(e => _userPaymentMethodServiceMapper.Map(e)).ToList()
            };
        }

        public DAL.App.DTO.PaymentMethod Map(PaymentMethod inObject)
        {
            return new DAL.App.DTO.PaymentMethod()
            {
                Id = inObject.Id,
                PaymentMethodCode = inObject.PaymentMethodCode,
                PaymentMethodDescription = inObject.PaymentMethodDescription,
                UserPaymentMethods = inObject.UserPaymentMethods
                    .Select(e => _userPaymentMethodServiceMapper.Map(e)).ToList()
            };        
        }
    }
}