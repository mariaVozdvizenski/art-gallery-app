using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Basket : Basket<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
    }
    
    public class Basket<TKey, TUser>: DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser> 
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public DateTime DateCreated { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}