using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IArtistRepository : IBaseRepository<Artist>
    {
        Task<IEnumerable<Artist>> AllAsync(Guid? userId = null);
        Task<Artist> FirstOrDefaultAsync(Guid? id, Guid? userId = null);
        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<ArtistDTO>> DTOAllAsync(Guid? userId = null);
        Task<ArtistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}