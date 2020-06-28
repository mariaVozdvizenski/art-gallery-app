using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.mavozd.Contracts.DAL.Base;using ee.itcollege.mavozd.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IArtistRepository :  IBaseRepository<Artist>
    {
    }
}