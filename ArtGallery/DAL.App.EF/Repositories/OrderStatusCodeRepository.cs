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
    }
}