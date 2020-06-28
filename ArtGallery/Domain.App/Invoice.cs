using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Domain.Base;


namespace Domain.App
{
    public class Invoice : DomainEntityIdMetadata
    {
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string InvoiceDetails { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid InvoiceStatusCodeId { get; set; } = default!;
        public InvoiceStatusCode? InvoiceStatusCode { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }
        
    }
}