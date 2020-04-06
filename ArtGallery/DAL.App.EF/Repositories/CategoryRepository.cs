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
    public class CategoryRepository : EFBaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<IEnumerable<Category>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }
            return await base.AllAsync(); // at first
            //return await RepoDbSet.Where(o => a.AppUserId == userId).ToListAsync();
        }

        public async Task<Category> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
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
            var category = await FirstOrDefaultAsync(id, userId);
            base.Remove(category);
        }
        
        public async Task<IEnumerable<CategoryDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                //query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    PaintingCount = c.CategoryPaintings.Count,
                })
                .ToListAsync();
        }

        public async Task<CategoryDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                //query = query.Where(a => a.AppUserId == userId);
            }
            
            var categoryDTO = await query.Select(o => new CategoryDTO()
            {
                Id = o.Id,
                CategoryName = o.CategoryName,
                PaintingCount = o.CategoryPaintings.Count
                
            }).FirstOrDefaultAsync();
            
            return categoryDTO;
        }
    }
}