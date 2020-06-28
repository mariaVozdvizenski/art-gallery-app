using System;
using ee.itcollege.mavozd.Contracts.Domain;

namespace ee.itcollege.mavozd.Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
        
    }
    
    public interface IBaseEntityTracker<TKey>
        where TKey: IEquatable<TKey>
    {
        //Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> EntityTracker { get;  }
        void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity);
    }
}