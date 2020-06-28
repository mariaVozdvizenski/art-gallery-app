using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using UserPaymentMethod = DAL.App.DTO.UserPaymentMethod;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserPaymentMethodRepository: IBaseRepository<UserPaymentMethod>
    {
    }
}