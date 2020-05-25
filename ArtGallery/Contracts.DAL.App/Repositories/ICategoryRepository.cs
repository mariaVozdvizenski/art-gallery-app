using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;
using Category = DAL.App.DTO.Category;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        
    }
}