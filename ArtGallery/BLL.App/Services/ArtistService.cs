using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace BLL.App.Services
{
    public class ArtistService : BaseEntityService<IArtistRepository, IAppUnitOfWork, Artist, Artist>, IArtistService
    {
        public ArtistService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<Artist, Artist>(), unitOfWork.Artists)
        {
        }

        public async Task<IEnumerable<Artist>> AllAsync(Guid? userId = null)
        {
            return await ServiceRepository.AllAsync(userId);
        }

        public async Task<Artist> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            return await ServiceRepository.FirstOrDefaultAsync(id, userId);
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            await ServiceRepository.DeleteAsync(id, userId);
        }

        public async Task<IEnumerable<ArtistDTO>> DTOAllAsync(Guid? userId = null)
        {
           return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<ArtistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
    }

}