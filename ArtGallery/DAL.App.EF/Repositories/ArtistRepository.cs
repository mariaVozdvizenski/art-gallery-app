using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class ArtistRepository : EFBaseRepository<Artist, AppDbContext>, IArtistRepository
    {
        public ArtistRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Artist>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }
            return await base.AllAsync(); // at first
            //return await RepoDbSet.Where(o => a.AppUserId == userId).ToListAsync();
        }

        public async Task<Artist> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }
            return await RepoDbSet.AnyAsync(a => a.Id == id);
            //return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }

        // we need to do it on database level, to avoid unnecessary queries to db 
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
    }
}