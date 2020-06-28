using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class UserPaymentMethodRepositoryMapper : IBaseMapper<UserPaymentMethod, DTO.UserPaymentMethod>
    {
        private readonly AppUserRepositoryMapper _appUserRepositoryMapper;
        private readonly PaymentMethodRepositoryMapper _paymentMethodRepositoryMapper;
        
        public UserPaymentMethodRepositoryMapper()
        {
            _appUserRepositoryMapper = new AppUserRepositoryMapper();
            _paymentMethodRepositoryMapper = new PaymentMethodRepositoryMapper();
        }
        
        public DTO.UserPaymentMethod Map(UserPaymentMethod inObject)
        {
            return new DTO.UserPaymentMethod()
            {
                AppUser = _appUserRepositoryMapper.Map(inObject.AppUser!),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                PaymentMethod = _paymentMethodRepositoryMapper.Map(inObject.PaymentMethod!),
                PaymentMethodId = inObject.PaymentMethodId
            };
        }

        public UserPaymentMethod Map(DTO.UserPaymentMethod inObject)
        {
            return new UserPaymentMethod()
            {
                AppUser = _appUserRepositoryMapper.Map(inObject.AppUser!),
                AppUserId = inObject.AppUserId,
                Id = inObject.Id,
                PaymentMethod = _paymentMethodRepositoryMapper.Map(inObject.PaymentMethod!),
                PaymentMethodId = inObject.PaymentMethodId
            };        
        }
    }
}