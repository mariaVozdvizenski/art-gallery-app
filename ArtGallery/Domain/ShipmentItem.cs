using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class ShipmentItem : ShipmentItem<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class ShipmentItem<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public Guid OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }
        
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}