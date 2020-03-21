using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Shipment: DomainEntity
    {
        [MaxLength(36)] 
        public string OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        [MaxLength(36)] 
        public string InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }

        public DateTime ShipmentDate { get; set; }

        public ICollection<ShipmentItem>? ShipmentItems { get; set; }
    }
}