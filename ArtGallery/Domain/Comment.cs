using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Comment: DomainEntityMetadata
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public string Body { get; set; } = default!;

        [MaxLength(36)] 
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)]
        public string PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
    }
}