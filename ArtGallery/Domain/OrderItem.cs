using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class OrderItem : OrderItem<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class OrderItem<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public ICollection<ShipmentItem>? ItemShipments { get; set; }
    }
}