using System;

namespace PublicApi.DTO.v1
{
    public class OrderItemView
    {
        public Guid Id { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        
        public string PaintingTitle { get; set; } = default!;
        public string ImageName { get; set; } = default!;
        public decimal PaintingPrice { get; set; }
        public Guid OrderId { get; set; } = default!;
        public int Quantity { get; set; }
    }
}