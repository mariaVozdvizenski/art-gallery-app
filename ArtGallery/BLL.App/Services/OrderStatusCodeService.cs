using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class OrderStatusCodeService : BaseEntityService<IAppUnitOfWork, IOrderStatusCodeRepository, IOrderStatusCodeServiceMapper,
    OrderStatusCode, DTO.OrderStatusCode>, IOrderStatusCodeService
    {
        public OrderStatusCodeService(IAppUnitOfWork uow) : base(uow, uow.OrderStatusCodes, new OrderStatusCodeServiceMapper())
        {
        }
    }
}