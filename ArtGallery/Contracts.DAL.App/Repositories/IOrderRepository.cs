using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using Order = DAL.App.DTO.Order;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository: IBaseRepository<Order>
    {
    }
}