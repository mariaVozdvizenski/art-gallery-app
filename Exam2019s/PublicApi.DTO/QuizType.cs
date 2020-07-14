using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class QuizType
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Type { get; set; } = default!;
    }
}