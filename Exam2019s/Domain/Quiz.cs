using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Quiz
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Title { get; set; } = default!;

        public Guid QuizTypeId { get; set; }
        public QuizType? QuizType { get; set; }

        public ICollection<Question>? QuizQuestions { get; set; }
    }
}