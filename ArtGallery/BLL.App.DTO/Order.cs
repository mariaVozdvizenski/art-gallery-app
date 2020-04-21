using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Order : Order<Guid>, IDomainBaseEntity
    {
    }

    public class Order<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public DateTime OrderDate { get; set; }
        
        public string? OrderDetails { get; set; }
        
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public TKey OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}