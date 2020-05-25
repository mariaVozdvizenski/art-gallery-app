using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Invoice : IDomainEntityId
    {
        public Guid Id { get; set; }
        
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