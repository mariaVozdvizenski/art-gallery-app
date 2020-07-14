using System;

namespace PublicApi.DTO
{
    public class QuizResult
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public int CorrectAnswers { get; set; }
    }
}