using System;
using ee.itcollege.mavozd.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Comment :  IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string CommentBody { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}