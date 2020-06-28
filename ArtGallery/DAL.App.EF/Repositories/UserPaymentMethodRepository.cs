using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.mavozd.DAL.Base.EF.Repositories;
using ee.itcollege.mavozd.DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UserPaymentMethod = Domain.App.UserPaymentMethod;

namespace DAL.App.EF.Repositories
{
    public class UserPaymentMethodRepository : EFBaseRepository<AppDbContext, AppUser, UserPaymentMethod, DTO.UserPaymentMethod>,
        IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<UserPaymentMethod, DTO.UserPaymentMethod>())
        {
        }

        public override async Task<IEnumerable<DTO.UserPaymentMethod>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(e => e.AppUser)
                .Include(e => e.PaymentMethod);

            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));

            return result;
        }
    }
}
