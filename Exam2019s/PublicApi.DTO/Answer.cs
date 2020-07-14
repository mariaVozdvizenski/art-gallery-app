using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class Answer
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(4096)]
        public string Content { get; set; } = default!;

        public Guid QuestionId { get; set; }

        public bool Correct { get; set; }
    }
}