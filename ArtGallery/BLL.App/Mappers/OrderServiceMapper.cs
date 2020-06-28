using System.Linq;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using Order = BLL.App.DTO.Order;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper : AppServiceBaseMapper<DAL.App.DTO.Order, Order>, IOrderServiceMapper
    {
    }
}