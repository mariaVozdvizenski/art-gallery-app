using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class ShipmentItemService : BaseEntityService<IAppUnitOfWork, IShipmentItemRepository, IShipmentItemServiceMapper,
    ShipmentItem, DTO.ShipmentItem>, IShipmentItemService
    {
        public ShipmentItemService(IAppUnitOfWork uow) : base(uow, uow.ShipmentItems, new ShipmentItemServiceMapper())
        {
        }
    }
}