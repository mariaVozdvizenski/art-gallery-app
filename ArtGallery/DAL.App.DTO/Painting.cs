using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Painting : Painting<Guid>, IDomainBaseEntity
    {
    }

    public class Painting<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        public string Title { get; set; } = default!;
        
        public string Size { get; set; } = default!;
        
        public TKey ArtistId { get; set; } = default!;
        public Artist? Artist { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PaintingCategory>? PaintingCategories { get; set; }
        public ICollection<BasketItem>? ItemBaskets { get; set; }
        public ICollection <OrderItem>? ItemOrders { get; set; }

    }
}