using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Shipment : Shipment<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Shipment<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public TKey InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        
        public DateTime ShipmentDate { get; set; }

        public ICollection<ShipmentItem>? ShipmentItems { get; set; }
    }
}