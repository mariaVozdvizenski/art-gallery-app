using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderItemRepository : EFBaseRepository<AppDbContext, Domain.OrderItem, DAL.App.DTO.OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<OrderItem, DTO.OrderItem>())
        {
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<DTO.OrderItem>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Painting)
                .AsQueryable();
            
            /*if (userId != null)
            {
                query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }*/
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.OrderItem> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Painting)
                .Where(b => b.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(item => item.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
    }
}