using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Comment : Comment<Guid>, IDomainBaseEntity
    {
    }

    public class Comment<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public string CommentBody { get; set; } = default!;
        
        public TKey AppUserId { get; set; } = default!;
        public AppUser<TKey>? AppUser { get; set; }
        
        public TKey PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}