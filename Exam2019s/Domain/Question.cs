﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Question
    {
        public Guid Id { get; set; }

        [MinLength(8)]
        [MaxLength(4096)]
        public string Content { get; set; } = default!;

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<Answer>? QuestionAnswers { get; set; }
    }
}