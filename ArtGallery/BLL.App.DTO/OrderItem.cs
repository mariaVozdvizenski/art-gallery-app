using System;
using System.Collections.Generic;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class OrderItem : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public ICollection<ShipmentItem>? ItemShipments { get; set; }

        public int Quantity { get; set; }
    }
}