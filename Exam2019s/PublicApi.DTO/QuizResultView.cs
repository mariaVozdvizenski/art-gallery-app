using System;

namespace PublicApi.DTO
{
    public class QuizResultView
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public int CorrectAnswers { get; set; }

        public int TotalAnswers { get; set; }
    }
}