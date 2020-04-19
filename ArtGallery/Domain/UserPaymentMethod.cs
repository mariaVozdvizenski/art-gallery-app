using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class UserPaymentMethod : UserPaymentMethod<Guid, AppUser>, IDomainEntityBaseMetadata,
        IDomainEntityUser<AppUser>
    {
        
    }
    public class UserPaymentMethod<TKey, TUser>: DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser> 
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}