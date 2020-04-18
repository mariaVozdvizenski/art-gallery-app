using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityBaseMetadata: IDomainEntityBaseMetadata<Guid>
    {
    }
    
    public interface IDomainEntityBaseMetadata<TKey> : IDomainBaseEntity<TKey>, IDomainMetadata
    where TKey : struct, IComparable
    {
    }
}