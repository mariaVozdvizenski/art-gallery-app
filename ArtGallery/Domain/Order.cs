using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order: DomainEntity
    {
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }
        
        [MaxLength(36)] 
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MaxLength(36)]
        public string OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}