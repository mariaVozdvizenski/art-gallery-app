using System;
using Domain.Base;

namespace Domain.App
{
    public class PaintingCategory : DomainEntityIdMetadata
    {
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}