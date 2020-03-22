using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BasketRepository : EFBaseRepository<Basket, AppDbContext>, IBasketRepository
    {
        public BasketRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}