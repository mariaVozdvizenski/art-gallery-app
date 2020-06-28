using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;

namespace BLL.App.Services
{
    public class BasketService : BaseEntityService<IAppUnitOfWork, IBasketRepository, IBasketServiceMapper, Basket, DTO.Basket>, 
        IBasketService
    {
        public BasketService(IAppUnitOfWork uow) : base(uow, uow.Baskets, new BasketServiceMapper())
        {
        }
        
        public async Task CreateBasketAsync(AppUser appUser)
        {
            var dalBasket = new Basket()
            {
                AppUserId = appUser.Id,
                DateCreated = DateTime.Now
            }; 
            Repository.Add(dalBasket);
            await UOW.SaveChangesAsync();
        }
    }
}