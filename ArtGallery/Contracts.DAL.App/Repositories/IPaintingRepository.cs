using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaintingRepository : IPaintingRepository<Guid, Painting>, IBaseRepository<Painting>
    {
    }
    public interface IPaintingRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity> 
        where TKey : IEquatable<TKey> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
    {
        Task<bool> ExistsAsync (Guid? id, Guid? userId = null);
        Task<IEnumerable<TDALEntity>> AllAsync (Guid? userId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        //Task<IEnumerable<PaintingDTO>> DTOAllAsync(Guid? userId = null);
        //Task<PaintingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);

    }
}