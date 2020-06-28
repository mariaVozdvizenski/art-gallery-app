using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IAddressService : IBaseEntityService<Address>
    {
        Task<bool> NoMoreThanThreeAddresses(Guid userGuidId);
    }
}