using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace BLL.App.Services
{
    public class PaintingService : BaseEntityService<IPaintingRepository, IAppUnitOfWork, DAL.App.DTO.Painting, BLL.App.DTO.Painting>, IPaintingService
    {
        public PaintingService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Painting, DTO.Painting>(), unitOfWork.Paintings)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        public async Task<IEnumerable<BLL.App.DTO.Painting>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity));
        }

        public async Task<BLL.App.DTO.Painting> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            await ServiceRepository.DeleteAsync(id, userId);
        }

        /*
        public async Task<IEnumerable<PaintingDTO>> DTOAllAsync(Guid? userId = null)
        {
            return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<PaintingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
        */
    }
}