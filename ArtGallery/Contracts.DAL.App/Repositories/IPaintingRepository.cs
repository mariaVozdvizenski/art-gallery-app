﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using Painting = DAL.App.DTO.Painting;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaintingRepository : IBaseRepository<Painting>, IPaintingRepositoryCustom
    {
    }
}