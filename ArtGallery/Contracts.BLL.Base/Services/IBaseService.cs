﻿using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService
    {
        // add common base methods here
    }

    public interface IBaseEntityService<TBLLEntity> : IBaseService, IBaseRepository<TBLLEntity>
        where TBLLEntity : class, IDomainEntityBaseMetadata<Guid>, new()
    {
    }
}