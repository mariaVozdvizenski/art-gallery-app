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
    public class PaintingRepository : EFBaseRepository<Painting, AppDbContext>, IPaintingRepository
    {
        public PaintingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Painting>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Artist)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Painting> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Artist)
                .Where(p => p.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
    }
}