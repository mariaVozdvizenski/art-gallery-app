using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Invoice : Invoice<Guid>, IDomainBaseEntity
    {
    }

    public class Invoice<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDetails { get; set; } = default!;
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public TKey InvoiceStatusCodeId { get; set; } = default!;
        public InvoiceStatusCode? InvoiceStatusCode { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }

    }
}