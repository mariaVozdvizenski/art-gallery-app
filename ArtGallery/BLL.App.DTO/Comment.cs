using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Comment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}