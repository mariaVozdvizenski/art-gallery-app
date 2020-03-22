using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaintingRepository : EFBaseRepository<Painting, AppDbContext>, IPaintingRepository
    {
        public PaintingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}