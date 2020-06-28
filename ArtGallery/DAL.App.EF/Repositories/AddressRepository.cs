using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using AppUser = Domain.App.Identity.AppUser;

namespace DAL.App.EF.Repositories
{
    public class AddressRepository : EFBaseRepository<AppDbContext, AppUser, Address, DAL.App.DTO.Address>,
        IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext) : base(dbContext,
            new AddressRepositoryMapper())
        {
        }

        public override async Task<IEnumerable<DTO.Address>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Include(e => e.AppUser);
            return await query.Select(e => Mapper.Map(e)).ToListAsync();
        }

        public override async Task<DTO.Address> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Include(e => e.AppUser);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            return Mapper.Map(domainEntity);
        }
    }
}