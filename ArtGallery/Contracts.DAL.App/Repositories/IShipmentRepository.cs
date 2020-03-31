using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IShipmentRepository: IBaseRepository<Shipment>
    {
        Task<bool> ExistsAsync (Guid? id, Guid? userId = null);
        Task<IEnumerable<Shipment>> AllAsync (Guid? userId = null);
        Task<Shipment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
    }
}