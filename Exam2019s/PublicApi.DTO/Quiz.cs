﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class Quiz
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Title { get; set; } = default!;

        public Guid QuizTypeId { get; set; }
    }
}