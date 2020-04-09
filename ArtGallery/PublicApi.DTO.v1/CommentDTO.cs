using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(4096)]
        [MinLength(1)]
        public string CommentBody { get; set; } = default!;

        public string PaintingTitle { get; set; } = default!;
    }
}