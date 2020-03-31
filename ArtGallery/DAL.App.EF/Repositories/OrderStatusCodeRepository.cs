using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderStatusCodeRepository : EFBaseRepository<OrderStatusCode, AppDbContext>, IOrderStatusCodeRepository
    {
        public OrderStatusCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(Guid? id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(o => o.Id == id);
        }
    }
}