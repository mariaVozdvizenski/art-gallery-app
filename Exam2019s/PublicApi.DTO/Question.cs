using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class Question
    {
        public Guid Id { get; set; }

        [MinLength(8)]
        [MaxLength(4096)]
        public string Content { get; set; } = default!;

        public Guid QuizId { get; set; }
    }
}