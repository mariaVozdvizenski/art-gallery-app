using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingCategoryRepository : BaseRepository<PaintingCategory>, IPaintingCategoryRepository
    {
        public PaintingCategoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}