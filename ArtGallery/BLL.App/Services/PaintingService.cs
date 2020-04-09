using System;
using System.Collections.Generic;
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
    public class PaintingService : BaseEntityService<IPaintingRepository, IAppUnitOfWork, Painting, Painting>, IPaintingService
    {
        public PaintingService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new IdentityMapper<Painting, Painting>(), unitOfWork.Paintings)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task<IEnumerable<Painting>> AllAsync(Guid? userId = null)
        {
            return await ServiceRepository.AllAsync(userId);
        }

        public async Task<Painting> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            return await ServiceRepository.FirstOrDefaultAsync(id, userId);
        }

        public async Task<IEnumerable<PaintingDTO>> DTOAllAsync(Guid? userId = null)
        {
            return await ServiceRepository.DTOAllAsync(userId);
        }

        public async Task<PaintingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        }
    }
}