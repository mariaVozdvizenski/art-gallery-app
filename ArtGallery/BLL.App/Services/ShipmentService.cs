using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class ShipmentService : BaseEntityService<IAppUnitOfWork, IShipmentRepository, IShipmentServiceMapper,
    Shipment, DTO.Shipment>, IShipmentService
    {
        public ShipmentService(IAppUnitOfWork uow) : base(uow, uow.Shipments, new ShipmentServiceMapper())
        {
        }
    }
}