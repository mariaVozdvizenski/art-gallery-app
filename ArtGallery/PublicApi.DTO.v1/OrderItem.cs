using System;

namespace PublicApi.DTO.v1
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;

        public int Quantity { get; set; }
    }
}