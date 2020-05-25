using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;

namespace PublicApi.DTO.v1
{
    public class Comment
    {
        public Guid Id { get; set; }
        
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;
        
        public Guid PaintingId { get; set; }
        public Guid AppUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
    }
}