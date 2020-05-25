using System;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class BLLCommentView 
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;

        public string CommentBody { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = default!;
        
    }
}