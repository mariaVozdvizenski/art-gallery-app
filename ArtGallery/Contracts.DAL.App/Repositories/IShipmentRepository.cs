using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IShipmentRepository: IBaseRepository<Shipment>
    {
        Task<Shipment> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid? id, Guid? userId = null);
        Task<IEnumerable<Shipment>> AllAsync(Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}