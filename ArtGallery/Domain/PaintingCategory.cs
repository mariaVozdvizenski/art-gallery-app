using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PaintingCategory : PaintingCategory<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class PaintingCategory<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}