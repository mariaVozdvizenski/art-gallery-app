using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Domain.Identity;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketRepository: IBaseRepository<Basket>
    {
         Task<Basket> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
         Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
         Task<IEnumerable<Basket>> AllAsync(Guid? userId = null);
         Task DeleteAsync(Guid id, Guid? userId = null);
         Task<IEnumerable<BasketDTO>> DTOAllAsync(Guid? userId = null);
         Task<BasketDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);

    }
}