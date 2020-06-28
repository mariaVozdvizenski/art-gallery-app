using System;

namespace PublicApi.DTO.v1
{
    public class BasketItemView
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public Guid BasketId { get; set; } = default!;

        public Guid PaintingId { get; set; } = default!;

        public string PaintingTitle { get; set; } = default!;

        public decimal PaintingPrice { get; set; }

        public string PaintingSize { get; set; } = default!;

        public int PaintingQuantity { get; set; }

        public string PaintingImageUrl { get; set; } = default!;
    }
}