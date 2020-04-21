using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class OrderItem : OrderItem<Guid>, IDomainBaseEntity
    {
    }

    public class OrderItem<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public ICollection<ShipmentItem>? ItemShipments { get; set; }
    }
}