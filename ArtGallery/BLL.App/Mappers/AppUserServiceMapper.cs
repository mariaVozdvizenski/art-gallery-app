using Contracts.BLL.Base.Mappers;
using DAL.App.DTO.Identity;

namespace BLL.App.Mappers
{
    public class AppUserServiceMapper : IBaseMapper<AppUser, DTO.Identity.AppUser>
    {
        public DTO.Identity.AppUser Map(AppUser inObject)
        {
            return new DTO.Identity.AppUser()
            {
                Id = inObject.Id,
                Email = inObject.Email,
                UserName = inObject.UserName
            };
        }

        public AppUser Map(DTO.Identity.AppUser inObject)
        {
            return new AppUser()
            {
                Email = inObject.Email,
                Id = inObject.Id,
                UserName = inObject.UserName
            };
        }
    }
}