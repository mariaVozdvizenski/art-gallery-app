using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PaintingCategory: DomainEntity
    {
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}