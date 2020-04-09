using System;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IArtistService Artists => 
            GetService<IArtistService>(() => new ArtistService(UnitOfWork));

        public IPaintingService Paintings => 
            GetService<IPaintingService>(() => new PaintingService(UnitOfWork));
    }
}