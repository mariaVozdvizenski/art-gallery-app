using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class BasketItem: DomainEntityMetadata
    {
        public int Quantity { get; set; }
        
        public DateTime DateCreated { get; set; }

        [MaxLength(36)]
        public string BasketId { get; set; } = default!;
        public Basket? Basket { get; set; }

        [MaxLength(36)]
        public string PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}