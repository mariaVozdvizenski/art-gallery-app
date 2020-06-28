using System.Linq;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using PaymentMethod = BLL.App.DTO.PaymentMethod;

namespace BLL.App.Mappers
{
    public class PaymentMethodServiceMapper : AppServiceBaseMapper<DAL.App.DTO.PaymentMethod, PaymentMethod>, IPaymentMethodServiceMapper
    {
    }
}