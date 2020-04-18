using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Comment: DomainEntityBaseMetadata
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}