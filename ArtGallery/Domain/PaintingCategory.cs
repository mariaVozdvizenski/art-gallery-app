using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PaintingCategory: DomainEntity
    {
        [MaxLength(36)]
        public string PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }

        [MaxLength(36)]
        public string CategoryId { get; set; } = default!;
        public Category? Category { get; set; }
    }
}