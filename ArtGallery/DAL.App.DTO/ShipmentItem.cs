using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class ShipmentItem : ShipmentItem<Guid>, IDomainBaseEntity
    {
        
    }
    public class ShipmentItem<TKey>: IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public Guid OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }
        
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}