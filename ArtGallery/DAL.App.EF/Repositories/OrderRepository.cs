﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Order>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.OrderStatusCode)
                .Include(o => o.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Order> FirstOrDefaultAsync(Guid? id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.OrderStatusCode)
                .Include(o => o.AppUser)
                .Where(o => o.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
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