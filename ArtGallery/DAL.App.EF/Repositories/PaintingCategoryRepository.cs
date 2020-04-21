using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingCategoryRepository : EFBaseRepository<AppDbContext, Domain.PaintingCategory, DTO.PaintingCategory>,
        IPaintingCategoryRepository
    {
        public PaintingCategoryRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<PaintingCategory, DTO.PaintingCategory>())
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(pc => pc.Id == id && pc.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(pc => pc.Id == id);
        }

        public async Task<IEnumerable<DTO.PaintingCategory>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.Category)
                .Include(pc => pc.Painting)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(pc => pc.AppUserId == userId);
            }
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.PaintingCategory> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.Category)
                .Include(pc => pc.Painting)
                .Where(pc => pc.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(pc => pc.AppUserId == userId);
            }
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
    }
}