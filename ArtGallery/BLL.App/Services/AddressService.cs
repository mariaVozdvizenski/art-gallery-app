using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using ee.itcollege.mavozd.BLL.Base.Services;

namespace BLL.App.Services
{
    public class AddressService :
        BaseEntityService<IAppUnitOfWork, IAddressRepository, IAddressServiceMapper, Address, DTO.Address>, IAddressService
    {
        public AddressService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Addresses, new AddressServiceMapper())
        {
        }

        public async Task<bool> NoMoreThanThreeAddresses(Guid userGuidId)
        {
            var userAddresses = await Repository.GetAllAsync(userGuidId);
            return userAddresses.Count() <= 2;
        }
    }
}