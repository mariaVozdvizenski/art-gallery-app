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
    public class CommentRepository : EFBaseRepository<Comment, AppDbContext>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<IEnumerable<Comment>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(c => c.Painting)
                .Include(c => c.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(c => c.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Comment> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(c => c.Painting)
                .Include(c => c.AppUser)
                .Where(c => c.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(c => c.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var comment = await FirstOrDefaultAsync(id, userId);
            base.Remove(comment);
        }

    }
}