using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityEntityBaseMetadata: IDomainEntityEntityBaseMetadata<Guid>
    {
    }
    
    public interface IDomainEntityEntityBaseMetadata<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
    where TKey : struct, IComparable
    {
    }
}