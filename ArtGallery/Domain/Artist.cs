using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Artist : Artist<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Artist<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MaxLength(128)] 
        [MinLength(1)] 
        public virtual string FirstName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public virtual string LastName { get; set; } = default!;
        
        [MaxLength(4096)] 
        [MinLength(1)] 
        public virtual string Bio { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public virtual string PlaceOfBirth { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public virtual string Country { get; set; } = default!;
        
        public virtual DateTime DateOfBirth { get; set; } = default!;
        
        public virtual ICollection<Painting>? Paintings { get; set; }

        public virtual string FirstLastName => FirstName + " " + LastName;
    }
}