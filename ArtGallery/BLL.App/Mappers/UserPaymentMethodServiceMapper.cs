using Contracts.BLL.App.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserPaymentMethodServiceMapper :  IUserPaymentMethodServiceMapper
    {
        private readonly AppUserServiceMapper _appUserServiceMapper;
        private readonly PaymentMethodServiceMapper _paymentMethodServiceMapper;
        public UserPaymentMethodServiceMapper()
        {
            _appUserServiceMapper = new AppUserServiceMapper();
            _paymentMethodServiceMapper = new PaymentMethodServiceMapper();
        }
        public DTO.UserPaymentMethod Map(UserPaymentMethod inObject)
        {
            return new DTO.UserPaymentMethod()
            {
                AppUser = _appUserServiceMapper.Map(inObject.AppUser!),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                PaymentMethod = _paymentMethodServiceMapper.Map(inObject.PaymentMethod!),
                PaymentMethodId = inObject.PaymentMethodId
            };
        }

        public UserPaymentMethod Map(DTO.UserPaymentMethod inObject)
        {
            return new UserPaymentMethod()
            {
                AppUser = _appUserServiceMapper.Map(inObject.AppUser!),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                PaymentMethod = _paymentMethodServiceMapper.Map(inObject.PaymentMethod!),
                PaymentMethodId = inObject.PaymentMethodId
            };        
        }
    }
}