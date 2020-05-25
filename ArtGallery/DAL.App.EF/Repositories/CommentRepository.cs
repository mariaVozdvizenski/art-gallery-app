using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Comment = Domain.App.Comment;

namespace DAL.App.EF.Repositories
{
    public class CommentRepository : EFBaseRepository<AppDbContext, AppUser, Comment , DAL.App.DTO.Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext, new CommentRepositoryMapper())
        {
        }
        public override async Task<IEnumerable<DTO.Comment>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(b => b.AppUser)
                .Include(b => b.Painting);

            var domainItems = await query.ToListAsync();

            var result = domainItems.Select(b => Mapper.Map(b));

            return result;
        }

        public override async Task<DTO.Comment> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking)
                .Include(e => e.AppUser);

            var domainEntity = await query
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            return Mapper.Map(domainEntity);
        }

        public async Task<IEnumerable<DALCommentView>> GetAllForViewAsync()
        {
            var query = RepoDbSet
                .Include(e => e.Painting)
                .Include(e => e.AppUser);

            var dalCommentViews =  await query.Select(e => new DALCommentView()
            {
                CommentBody = e.CommentBody,
                CreatedAt = DateTime.Now,
                CreatedBy = e.AppUser.Email,
                Id = e.Id,
                AppUserId = e.AppUserId
                
            }).ToListAsync();

            return dalCommentViews;
        }
    }
}