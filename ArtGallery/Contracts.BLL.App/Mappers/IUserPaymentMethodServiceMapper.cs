﻿using ee.itcollege.mavozd.Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IUserPaymentMethodServiceMapper : IBaseMapper<UserPaymentMethod, BLLAppDTO.UserPaymentMethod>
    {
    }
}