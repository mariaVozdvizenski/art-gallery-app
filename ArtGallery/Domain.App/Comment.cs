using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Comment : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}