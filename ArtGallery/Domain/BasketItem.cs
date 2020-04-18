using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class BasketItem: DomainEntityEntityBaseMetadata
    {
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid BasketId { get; set; } = default!;
        public Basket? Basket { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}