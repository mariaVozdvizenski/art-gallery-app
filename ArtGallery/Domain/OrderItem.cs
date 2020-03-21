using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class OrderItem: DomainEntity
    {
        public string PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }

        [MaxLength(36)]
        public string OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public ICollection<ShipmentItem>? ItemShipments { get; set; }
    }
}