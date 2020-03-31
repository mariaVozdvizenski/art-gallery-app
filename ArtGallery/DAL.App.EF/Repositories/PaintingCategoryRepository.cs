using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingCategoryRepository : EFBaseRepository<PaintingCategory, AppDbContext>,
        IPaintingCategoryRepository
    {
        public PaintingCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(pc => pc.Id == id);
        }

        public async Task<IEnumerable<PaintingCategory>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.Category)
                .Include(pc => pc.Painting)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<PaintingCategory> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(pc => pc.Category)
                .Include(pc => pc.Painting)
                .Where(pc => pc.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
    }
}