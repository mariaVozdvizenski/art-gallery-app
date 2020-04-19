using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Invoice : Invoice<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Invoice<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string InvoiceDetails { get; set; } = default!;
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public TKey InvoiceStatusCodeId { get; set; } = default!;
        public InvoiceStatusCode? InvoiceStatusCode { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }
    }
}