using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }

        public Guid OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}