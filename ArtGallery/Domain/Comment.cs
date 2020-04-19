using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Comment : Comment<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser>
    {
        
    }
    public class Comment<TKey, TUser>: DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser> 
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;
        
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}