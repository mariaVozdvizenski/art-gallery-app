using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BasketItemRepository : EFBaseRepository<BasketItem, AppDbContext>, IBasketItemRepository
    {
        public BasketItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        /*public IEnumerable<BasketItem> Include(Expression<Func<BasketItem, object>> criteria)
        {
            return RepoDbSet.Include(criteria);
        }*/
    }
}