using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public DateTime OrderDate { get; set; }
        
        public string? OrderDetails { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser AppUser { get; set; }
        
        public Guid OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}