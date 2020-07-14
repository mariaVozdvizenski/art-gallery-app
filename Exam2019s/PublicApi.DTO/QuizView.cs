using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class QuizView
    {
        public Guid Id { get; set; }

        [MinLength(1)] [MaxLength(128)] public string Title { get; set; } = default!;

        public Guid QuizTypeId { get; set; }
        public string QuizType { get; set; } = default!;

        public int HowManyTimesDone { get; set; }

        public int CompletelyCorrectAnswers { get; set; }
        
        public ICollection<QuestionView>? QuizQuestionViews { get; set; }
    }
}