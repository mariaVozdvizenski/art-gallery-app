using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.Mappers;
using Artist = Domain.App.Artist;

namespace DAL.App.EF.Repositories
{
    public class ArtistRepository : EFBaseRepository<AppDbContext, AppUser, Artist, DAL.App.DTO.Artist>, IArtistRepository
    {
        public ArtistRepository(AppDbContext dbContext) : base(dbContext, 
            new ArtistRepositoryMapper())
        {
        }

        public override async Task<IEnumerable<DTO.Artist>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Include(e => e.Paintings);
            return await query.Select(e => Mapper.Map(e)).ToListAsync();
        }

        public override async Task<DTO.Artist> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Include(e => e.Paintings);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            return Mapper.Map(domainEntity);
        }
    }
}