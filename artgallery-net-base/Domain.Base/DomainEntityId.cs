using System;
using ee.itcollege.mavozd.Contracts.Domain;

namespace ee.itcollege.mavozd.Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
    {
        
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
    
}