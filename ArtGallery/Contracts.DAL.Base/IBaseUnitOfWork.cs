using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        IBaseRepository<TDALEntity> FindRepository<TDALEntity>()
            where TDALEntity : class, IDomainEntity<Guid>, new();
    }
}