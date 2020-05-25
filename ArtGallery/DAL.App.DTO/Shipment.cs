using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Shipment : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        
        public DateTime ShipmentDate { get; set; }

        public ICollection<ShipmentItem>? ShipmentItems { get; set; }
    }
}