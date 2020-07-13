using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class QuizType
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Type { get; set; } = default!;
        
        public ICollection<Quiz>? QuizTypeQuizzes { get; set; }
    }
}