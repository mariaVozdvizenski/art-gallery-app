using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Shipment : Shipment<Guid>, IDomainBaseEntity
    {
    }
    
    public class Shipment<TKey>: IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public TKey InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        
        public DateTime ShipmentDate { get; set; }

        public ICollection<ShipmentItem>? ShipmentItems { get; set; }
    }
}