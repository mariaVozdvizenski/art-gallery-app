using System;

namespace Contracts.DAL.Base
{
    public interface IDomainBaseEntity : IDomainBaseEntity<Guid>
    {
    }
    public interface IDomainBaseEntity<TKey>
    where TKey : IEquatable<TKey>
    {
         TKey Id { get; set; }
    }
}