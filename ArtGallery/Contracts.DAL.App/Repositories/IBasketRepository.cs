using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using Basket = DAL.App.DTO.Basket;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketRepository: IBaseRepository<Basket>
    {
        Task<Basket> GetRightBasketForUserAsync(Guid userId, bool noTracking = true);
    }
}