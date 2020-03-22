using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserPaymentMethodRepository : EFBaseRepository<UserPaymentMethod, AppDbContext>, IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}