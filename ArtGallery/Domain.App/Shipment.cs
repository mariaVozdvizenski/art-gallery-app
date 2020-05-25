using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class Shipment : DomainEntityIdMetadata
    {
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        
        public DateTime ShipmentDate { get; set; }

        public ICollection<ShipmentItem>? ShipmentItems { get; set; }
        
    }
}