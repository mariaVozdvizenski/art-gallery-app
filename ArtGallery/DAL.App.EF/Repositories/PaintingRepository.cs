using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Painting = Domain.App.Painting;

namespace DAL.App.EF.Repositories
{
    public class PaintingRepository : EFBaseRepository<AppDbContext, AppUser ,Painting, DTO.Painting>, IPaintingRepository
    {
        public PaintingRepository(AppDbContext dbContext) : base(dbContext, new PaintingRepositoryMapper())
        {
        }

        public override async Task<IEnumerable<DTO.Painting>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(e => e.Artist);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));

            return result;
        }

        public override async Task<DTO.Painting> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var domainPainting = await query.FirstOrDefaultAsync(e => e.Id == id);
            var result = Mapper.Map(domainPainting);

            return result;
        }

        public async Task<IEnumerable<DALPaintingView>> GetAllForViewAsync()
        {
            var query = RepoDbSet
                .Include(e => e.Artist)
                .Include(e => e.Comments);

            var result =  await query.Select(e => new DALPaintingView()
            {
                Id = e.Id,
                ArtistId = e.ArtistId,
                ArtistName = e.Artist!.FirstName + " " + e.Artist.LastName,
                Description = e.Description,
                Price = e.Price,
                Size = e.Size,
                Title = e.Title,
                Quantity = e.Quantity,
                Comments = e.Comments.Select(e => new DALCommentView()
                {
                    CommentBody = e.CommentBody,
                    CreatedAt = e.CreatedAt,
                    CreatedBy = e.AppUser!.Email,
                    Id = e.Id
                }).ToList()
                
            }).ToListAsync();

            return result;
        }

        public async Task<DALPaintingView> GetFirstOrDefaultForViewAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .Include(e => e.Artist)
                .Include(e => e.Comments)
                .Where(e => e.Id == id)
                .Select(e => new DALPaintingView()
                {
                    Id = e.Id,
                    ArtistId = e.ArtistId,
                    ArtistName = e.Artist!.FirstName + " " + e.Artist.LastName,
                    Description = e.Description,
                    Price = e.Price,
                    Size = e.Size,
                    Title = e.Title,
                    Quantity = e.Quantity,
                    Comments = e.Comments.Select(e => new DALCommentView()
                    {
                        CommentBody = e.CommentBody,
                        CreatedAt = e.CreatedAt,
                        CreatedBy = e.AppUser!.Email,
                        Id = e.Id
                    }).ToList()
                    
                }).FirstOrDefaultAsync();
        }
    }
}
