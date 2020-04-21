using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class BasketItem : BasketItem<Guid>, IDomainBaseEntity
    {
        
    }

    public class BasketItem<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public int Quantity { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public TKey BasketId { get; set; } = default!;
        public Basket? Basket { get; set; }
        
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}