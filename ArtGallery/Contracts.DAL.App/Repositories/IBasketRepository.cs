using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IBasketRepository: IBaseRepository<Basket>
    {
         Task<Basket> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
         Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
         Task<IEnumerable<Basket>> AllAsync(Guid? id, Guid? userId = null);
         Task<IEnumerable<AppUser>> GetUsers();

    }
}