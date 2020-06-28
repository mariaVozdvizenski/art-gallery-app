using System;
using ee.itcollege.mavozd.Domain.Base;


namespace Domain.App
{
    public class ShipmentItem : DomainEntityIdMetadata
    {
        public Guid OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }
        
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}