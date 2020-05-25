using Contracts.DAL.Base.Mappers;
using Domain.App.Identity;

namespace DAL.App.EF.Mappers
{
    public class AppUserRepositoryMapper : IBaseMapper<AppUser, DTO.Identity.AppUser>
    {
        public DTO.Identity.AppUser Map(AppUser inObject)
        {
            return new DTO.Identity.AppUser()
            {
                Email = inObject.Email,
                Id = inObject.Id, 
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