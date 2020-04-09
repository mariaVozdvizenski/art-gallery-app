using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TBLLEntity, TDALEntity ,TUnitOfWork> : BaseService, IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainEntity<Guid>, new() 
        where TDALEntity : class, IDomainEntity<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork ServiceUnitOfWork;
        private readonly IBaseBLLMapper _mapper;
        protected readonly IBaseRepository<TDALEntity> ServiceRepository;
        
        public BaseEntityService(TUnitOfWork unitOfWork, IBaseBLLMapper mapper)
        {
            ServiceUnitOfWork = unitOfWork;
            _mapper = mapper;
            // TODO
            ServiceRepository = ServiceUnitOfWork.FindRepository<TDALEntity>();
        }

        public virtual IEnumerable<TBLLEntity> All()
        {
            return ServiceRepository.All().Select(entity => _mapper.Map<TDALEntity, TBLLEntity>(entity));
        }

        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync()
        {
            return (await ServiceRepository.AllAsync()).Select(entity => _mapper.Map<TDALEntity, TBLLEntity>(entity));
        }

        public virtual TBLLEntity Find(params object[] id)
        {
            return _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Find(id));
        }

        public virtual async Task<TBLLEntity> FindAsync(params object[] id)
        {
            return _mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.FindAsync(id));        }

        public virtual TBLLEntity Add(TBLLEntity entity)
        {
            return 
                _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Add(_mapper.Map<TBLLEntity, TDALEntity>(entity)));
        }

        public virtual TBLLEntity Update(TBLLEntity entity)
        {
            return 
                _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Update(_mapper.Map<TBLLEntity, TDALEntity>(entity)));        
        }

        public virtual TBLLEntity Remove(TBLLEntity entity)
        {
            return 
                _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(_mapper.Map<TBLLEntity, TDALEntity>(entity)));
            
        }

        public virtual TBLLEntity Remove(params object[] id)
        {
            return _mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(id));
        }
    }
}