using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class PaintingCategory : PaintingCategory<Guid>, IDomainBaseEntity
    {
    }

    public class PaintingCategory<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public TKey CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}