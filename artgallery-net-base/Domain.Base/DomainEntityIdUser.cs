using System;
using System.Text.Json.Serialization;
using ee.itcollege.mavozd.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace ee.itcollege.mavozd.Domain.Base
{
    public abstract class DomainEntityIdUser<TUser> : DomainEntityIdUser<Guid, TUser>, IDomainEntityId, IDomainEntityUser<TUser> 
        where TUser : IdentityUser<Guid>
    {
    }

    public abstract class DomainEntityIdUser<TKey, TUser> : DomainEntityId<TKey>, IDomainEntityUser<TKey, TUser> 
        where TKey : IEquatable<TKey> 
        where TUser : IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        
        [JsonIgnore] 
        public TUser? AppUser { get; set; }
    }
}