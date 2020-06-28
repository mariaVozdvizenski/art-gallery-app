using System;

namespace ee.itcollege.mavozd.Contracts.Domain
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
        
    }
    
    public interface IDomainEntityId<TKey>
        where TKey: IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
    
}