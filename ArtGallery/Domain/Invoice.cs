using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Invoice: DomainEntity
    {
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string InvoiceDetails { get; set; } = default!;

        [MaxLength(36)]
        public string OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        [MaxLength(36)]
        public string InvoiceStatusCodeId { get; set; } = default!;
        public InvoiceStatusCode? InvoiceStatusCode { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }
    }
}