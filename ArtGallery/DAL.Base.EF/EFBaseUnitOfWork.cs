using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork, IBaseUnitOfWork
    where TDbContext : DbContext
    {
        protected TDbContext UOWDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UOWDbContext = uowDbContext;
        }

        public int SaveChanges()
        {
           return UOWDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();
        }

        public IBaseRepository<TDALEntity> FindRepository<TDALEntity>() 
            where TDALEntity : class, IDomainEntity<Guid>, new()
        {
            throw new NotImplementedException();
        }
    }
}