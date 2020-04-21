using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class OrderStatusCode : OrderStatusCode<Guid>, IDomainBaseEntity
    {
    }

    public class OrderStatusCode<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
       
        public string OrderStatusDescription { get; set; } = default!;

        public string Code { get; set; } = default!;

        public ICollection<Order>? Orders { get; set; }
    }
    
}