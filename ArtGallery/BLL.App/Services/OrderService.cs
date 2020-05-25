using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, Order, DTO.Order>,
        IOrderService
    {
        public OrderService(IAppUnitOfWork uow) : base(uow, uow.Orders, new OrderServiceMapper())
        {
        }
    }
}