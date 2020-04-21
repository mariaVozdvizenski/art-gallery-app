using System;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Artist = BLL.App.DTO.Artist;

namespace Contracts.BLL.App.Services
{
    public interface IPaintingService : IPaintingRepository<Guid, Painting>
    {
        
    }
}