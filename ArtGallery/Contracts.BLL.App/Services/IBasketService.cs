using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;
using Domain.App.Identity;

namespace Contracts.BLL.App.Services
{
    public interface IBasketService : IBaseEntityService<Basket>
    {
        Task CreateBasketAsync(AppUser appUser);
        //Task<Basket> GetRightBasketForUser(object? userId = null);
    }
}