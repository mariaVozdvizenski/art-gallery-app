using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using DAL.App.DTO;

namespace DAL.App.EF.Repositories
{
    public class ArtistRepository : EFBaseRepository<AppDbContext, Domain.Artist, DAL.App.DTO.Artist>, IArtistRepository
    {
        //TODO: Create DAL.App.DTO thingies
        public ArtistRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseDALMapper<Domain.Artist, DAL.App.DTO.Artist>())
        {
        }
        public async Task<IEnumerable<DAL.App.DTO.Artist>> AllAsync(Guid? userId = null)
        {
            if (userId != null)
            {
                /*return (await RepoDbSet.Where(o => o.AppUserId == userId)
                    .Select(dbEntity => new ArtistDisplay()
                {
                    Bio = dbEntity.Bio,
                    Id = dbEntity.Id,
                    Country = dbEntity.Country,
                    DateOfBirth = dbEntity.DateOfBirth,
                    FirstName = dbEntity.FirstName,
                    LastName = dbEntity.LastName,
                    PlaceOfBirth = dbEntity.PlaceOfBirth,
                    PaintingCount = dbEntity.Paintings.Count
                    
                }).ToListAsync()).Select(dbEntity => Mapper.Map<ArtistDisplay, DAL.App.DTO.Artist>(dbEntity));
                */
            }
            return (await RepoDbSet.ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Artist> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                //return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }

            var artist = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(artist.Id);
        }

        /*
        public async Task<IEnumerable<ArtistDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                //query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(o => new ArtistDTO()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Country = o.Country,
                    PaintingCount = o.Paintings.Count
                })
                .ToListAsync();
        }

        public async Task<ArtistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }

            var artistDto = await query.Select(o => new ArtistDTO()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Country = o.Country,
                PaintingCount = o.Paintings.Count
            }).FirstOrDefaultAsync();

            return artistDto;
        }
        */
    }
}