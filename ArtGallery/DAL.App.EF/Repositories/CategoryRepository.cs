using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : EFBaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<bool> ExsistsAsync(Guid id, Guid? userId = null)
        {
            /*if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }*/
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }
    }
}