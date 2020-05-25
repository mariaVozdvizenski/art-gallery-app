using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CommentView
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }

        public string CommentBody { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = default!;
    }
}