using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingCategoryRepository : EFBaseRepository<AppDbContext, AppUser, PaintingCategory, DTO.PaintingCategory>,
        IPaintingCategoryRepository
    {
        public PaintingCategoryRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<PaintingCategory, DTO.PaintingCategory>())
        {
        }

        public override async Task<IEnumerable<DTO.PaintingCategory>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(e => e.Category)
                .Include(e => e.Painting);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));

            return result;
        }
    }
}