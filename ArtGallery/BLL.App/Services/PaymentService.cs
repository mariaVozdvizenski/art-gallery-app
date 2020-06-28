using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class PaymentService : BaseEntityService<IAppUnitOfWork, IPaymentRepository, IPaymentServiceMapper,
    Payment, DTO.Payment>, IPaymentService
    {
        public PaymentService(IAppUnitOfWork uow) : base(uow, uow.Payments, new PaymentServiceMapper())
        {
        }
    }
}