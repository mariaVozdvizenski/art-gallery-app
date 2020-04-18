using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntityEntityBaseMetadata :  DomainEntityEntityBaseMetadata<Guid>
    {
    }

    public abstract class DomainEntityEntityBaseMetadata<TKey> :  IDomainEntityEntityBaseMetadata<TKey> 
        where TKey : struct, IComparable
    {
        public virtual TKey Id { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
    }
}