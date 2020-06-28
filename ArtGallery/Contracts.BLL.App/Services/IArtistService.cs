using System;
using BLL.App.DTO;
using ee.itcollege.mavozd.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IArtistService : IBaseEntityService<Artist>
    {
    }
}