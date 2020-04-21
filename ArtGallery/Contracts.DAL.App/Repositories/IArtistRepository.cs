using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IArtistRepository : IArtistRepository<Guid, Artist>, IBaseRepository<Artist>
    {
    }
    
    public interface IArtistRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
        where TKey : IEquatable<TKey> 
    {
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? userId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        //Task<IEnumerable<ArtistDTO>> DTOAllAsync(Guid? userId = null);
        //Task<ArtistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}