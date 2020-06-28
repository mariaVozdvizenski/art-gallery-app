using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Painting : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        public string ImageName { get; set; } = default!;

        public string Title { get; set; } = default!;
        
        public string Size { get; set; } = default!;

        public int Quantity { get; set; }
        
        public Guid ArtistId { get; set; } = default!;
        public Artist? Artist { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PaintingCategory>? PaintingCategories { get; set; }
        public ICollection<BasketItem>? ItemBaskets { get; set; }
        public ICollection <OrderItem>? ItemOrders { get; set; }
    }
}