using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IShipmentItemRepository: IBaseRepository<ShipmentItem>
    {
        Task<bool> ExistsAsync (Guid? id, Guid? userId = null);
        Task<IEnumerable<ShipmentItem>> AllAsync (Guid? userId = null);
        Task<ShipmentItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        
    }
}