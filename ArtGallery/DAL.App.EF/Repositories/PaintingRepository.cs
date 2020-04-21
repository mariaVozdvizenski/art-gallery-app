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

namespace DAL.App.EF.Repositories
{
    public class PaintingRepository : EFBaseRepository<AppDbContext ,Domain.Painting, DTO.Painting>, IPaintingRepository
    {
        public PaintingRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Painting, DTO.Painting>())
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<DTO.Painting>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Artist)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Painting> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Artist)
                .Where(p => p.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(p => p.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var painting = await FirstOrDefaultAsync(id, userId);
            base.Remove(painting);
        }

        /*
        public async Task<IEnumerable<PaintingDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Artist)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(o => o.Animal!.AppUserId == userId && o.Owner!.AppUserId == userId);
            }
           return await query
                .Select(o => new PaintingDTO()
                {
                    Id = o.Id,
                    Title = o.Title,
                    Size = o.Size,
                    Price = o.Price,
                    ArtistId = o.ArtistId,
                    Artist = new ArtistDTO()
                    {
                        Id = o.Artist!.Id,
                        Country = o.Artist!.Country,
                        FirstName = o.Artist!.FirstName,
                        LastName = o.Artist!.LastName,
                        PaintingCount = o.Artist.Paintings.Count
                    },
                }).ToListAsync();
        }

        public async Task<PaintingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Artist)
                .Where(o => o.Id == id)
                .AsQueryable();
            
             if (userId != null)
             {
                 //query = query.Where(o => o.Animal!.AppUserId == userId && o.Owner!.AppUserId == userId);
             }
             
            var paintingDTO = await query.Select(o => new PaintingDTO()
            {
                Id = o.Id,
                Title = o.Title,
                Size = o.Size,
                Price = o.Price,
                Artist = new ArtistDTO()
                {
                    Id = o.Artist!.Id,
                    Country = o.Artist!.Country,
                    FirstName = o.Artist!.FirstName,
                    LastName = o.Artist!.LastName,
                    PaintingCount = o.Artist.Paintings.Count,
                },
            }).FirstOrDefaultAsync();
            
            return paintingDTO;
        }
        */
    
    }
}
