using System.Linq;
using Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class PaymentMethodRepositoryMapper : IBaseMapper<PaymentMethod, DTO.PaymentMethod>
    {
        private readonly UserPaymentMethodRepositoryMapper _userPaymentMethodRepositoryMapper;
        public PaymentMethodRepositoryMapper()
        {
            _userPaymentMethodRepositoryMapper = new UserPaymentMethodRepositoryMapper();
        }
        public DTO.PaymentMethod Map(PaymentMethod inObject)
        {
            return new DTO.PaymentMethod()
            {
                Id = inObject.Id,
                PaymentMethodCode = inObject.PaymentMethodCode,
                PaymentMethodDescription = inObject.PaymentMethodDescription,
                UserPaymentMethods = inObject.UserPaymentMethods
                    .Select(e => _userPaymentMethodRepositoryMapper.Map(e)).ToList()
            };
        }

        public PaymentMethod Map(DTO.PaymentMethod inObject)
        {
            return new PaymentMethod()
            {
                Id = inObject.Id,
                PaymentMethodCode = inObject.PaymentMethodCode,
                PaymentMethodDescription = inObject.PaymentMethodDescription,
                UserPaymentMethods = inObject.UserPaymentMethods
                    .Select(e => _userPaymentMethodRepositoryMapper.Map(e)).ToList(),
            };        
        }
    }
}