using System;

namespace Domain
{
    public class QuizResult
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public int CorrectAnswers { get; set; }
    }
}