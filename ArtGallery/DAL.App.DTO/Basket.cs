using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Basket : Basket<Guid>, IDomainBaseEntity
    {
        
    }

    public class Basket<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public DateTime DateCreated { get; set; }
        
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}