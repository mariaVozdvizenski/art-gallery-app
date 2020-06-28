using System;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ShipmentItem : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }
        
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}