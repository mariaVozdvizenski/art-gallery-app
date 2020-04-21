using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Artist : Artist<Guid>, IDomainBaseEntity
    {
    }

    public class Artist<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public virtual string FirstName { get; set; } = default!;
        public virtual string LastName { get; set; } = default!;
        
        public virtual string Bio { get; set; } = default!;

        public virtual string PlaceOfBirth { get; set; } = default!;
        public virtual string Country { get; set; } = default!;
        
        public virtual DateTime DateOfBirth { get; set; } = default!;
        
        public virtual ICollection<Painting>? Paintings { get; set; }
        
    }
   
}