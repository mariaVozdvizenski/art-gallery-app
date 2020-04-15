using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CommentCreateDTO
    {
        [MaxLength(4096)] [MinLength(1)] 
        public string CommentBody { get; set; } = default!;

        public Guid PaintingId { get; set; } 
    }
}