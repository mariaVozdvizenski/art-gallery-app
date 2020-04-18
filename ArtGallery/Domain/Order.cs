using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order: DomainEntityBaseMetadata
    {
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}