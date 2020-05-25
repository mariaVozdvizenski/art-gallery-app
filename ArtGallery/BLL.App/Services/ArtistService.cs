using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using Domain.App.Identity;
using PublicApi.DTO.v1;
using Artist = DAL.App.DTO.Artist;

namespace BLL.App.Services
{
    public class ArtistService :
        BaseEntityService<IAppUnitOfWork, IArtistRepository, IArtistServiceMapper, Artist, DTO.Artist>, IArtistService
    {
        public ArtistService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Artists, new ArtistServiceMapper())
        {
        }

        public override Task<IEnumerable<DTO.Artist>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            return base.GetAllAsync(userId, noTracking);
        }
    }
}
    