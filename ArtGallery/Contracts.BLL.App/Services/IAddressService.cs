using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IAddressService : IBaseEntityService<Address>
    {
        Task<bool> NoMoreThanThreeAddresses(Guid userGuidId);
    }
}