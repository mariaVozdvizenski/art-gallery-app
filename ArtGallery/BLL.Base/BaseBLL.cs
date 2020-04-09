using System;
using System.Collections;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
    where TUnitOfWork: IBaseUnitOfWork
    {
        protected readonly TUnitOfWork UnitOfWork;
        
        public BaseBLL(TUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await UnitOfWork.SaveChangesAsync();
        }
        
        public int SaveChanges()
        {
            return UnitOfWork.SaveChanges();
        }
    }
}