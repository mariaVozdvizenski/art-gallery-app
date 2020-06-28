using System;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PaintingCategory : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}