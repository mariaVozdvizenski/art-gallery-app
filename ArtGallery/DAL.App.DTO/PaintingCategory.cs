using System;
using ee.itcollege.mavozd.Contracts.Domain;

namespace DAL.App.DTO
{
    public class PaintingCategory : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}