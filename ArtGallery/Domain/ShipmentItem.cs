using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class ShipmentItem: DomainEntity
    {
        [MaxLength(36)]
        public string OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }

        [MaxLength(36)]
        public string ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}