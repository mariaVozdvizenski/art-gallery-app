using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
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