using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class BasketItem : BasketItem<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class BasketItem<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public TKey BasketId { get; set; } = default!;
        public Basket? Basket { get; set; }
        
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}