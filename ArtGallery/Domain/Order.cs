using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Order : Order<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
        
    }
    public class Order<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser> 
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }
        
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        public TKey OrderStatusCodeId { get; set; } = default!;
        public OrderStatusCode? OrderStatusCode { get; set; }

        public ICollection<Shipment>? Shipments { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}