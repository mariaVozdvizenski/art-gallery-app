using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class BasketItem :  IDomainEntityId
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public Guid BasketId { get; set; } = default!;
        public Basket? Basket { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
    
}