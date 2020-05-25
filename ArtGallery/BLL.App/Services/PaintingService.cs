using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App.Identity;
using PublicApi.DTO.v1;
using Painting = DAL.App.DTO.Painting;

namespace BLL.App.Services
{
    public class PaintingService : 
        BaseEntityService<IAppUnitOfWork, IPaintingRepository, IPaintingServiceMapper, Painting, DTO.Painting>, IPaintingService
    {
        public PaintingService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, unitOfWork.Paintings, new PaintingServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLPaintingView>> GetAllForViewAsync()
        {
            var query = await Repository.GetAllForViewAsync();
            var result = query.Select(e => Mapper.MapPaintingView(e));
            return result;
        }

        public async Task<BLLPaintingView> GetFirstOrDefaultForViewAsync(Guid id, Guid? userId = null)
        {
            var dalPaintingView = await Repository.GetFirstOrDefaultForViewAsync(id, userId);
            return Mapper.MapPaintingView(dalPaintingView);
        }
    }
}