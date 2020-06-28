using System;

namespace PublicApi.DTO.v1
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; }
        
        public DateTime? DateCreated { get; set; }
        
        public Guid BasketId { get; set; } = default!;

        public Guid PaintingId { get; set; } = default!;
    }
}