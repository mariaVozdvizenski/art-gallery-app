using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ArtistService : BaseEntityService<IArtistRepository, IAppUnitOfWork, DAL.App.DTO.Artist, BLL.App.DTO.Artist>, IArtistService
    {
        public ArtistService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Artist, DTO.Artist>(), unitOfWork.Artists)
        {
        }

        public async Task<IEnumerable<BLL.App.DTO.Artist>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity));
        }
        
        public async Task<BLL.App.DTO.Artist> FirstOrDefaultAsync(Guid? id, Guid? userId = null) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);
        }

        /*
        public async Task<IEnumerable<ArtistDTO>> DTOAllAsync(Guid? userId = null)
        {
           return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<ArtistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
        */
    }
    