using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaintingRepositoryCustom : IPaintingRepositoryCustom<DALPaintingView>
    {
    }
    
    public interface IPaintingRepositoryCustom<TPaintingView>
    {
        Task<IEnumerable<TPaintingView>> GetAllForViewAsync();
        Task<TPaintingView> GetFirstOrDefaultForViewAsync(Guid id, Guid? userId = null);
    }
}