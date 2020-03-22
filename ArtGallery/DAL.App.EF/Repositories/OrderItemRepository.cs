using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderItemRepository : EFBaseRepository<OrderItem, AppDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}