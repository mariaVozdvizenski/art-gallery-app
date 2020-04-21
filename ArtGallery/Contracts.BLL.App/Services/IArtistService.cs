using System;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IArtistService: IArtistRepository<Guid, Artist>
    {
            // TODO: add custom methods
    }
}