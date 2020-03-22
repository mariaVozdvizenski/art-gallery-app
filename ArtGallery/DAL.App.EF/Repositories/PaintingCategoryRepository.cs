using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingCategoryRepository : EFBaseRepository<PaintingCategory, AppDbContext>, IPaintingCategoryRepository
    {
        public PaintingCategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}