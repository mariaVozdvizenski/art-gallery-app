using System.Linq;
using ee.itcollege.mavozd.Contracts.DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Mappers
{
    public class BasketRepositoryMapper : AppDALBaseMapper<Basket, DTO.Basket>
    {
        public BasketRepositoryMapper()
        {
        }
        
    }
}