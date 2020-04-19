using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Category : Category<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Category<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MaxLength(36)] 
        [MinLength(1)] 
        public string CategoryName { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}