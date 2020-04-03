using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CategoryEditDTO
    {
        public Guid Id { get; set; }

        [MaxLength(36)]
        [MinLength(1)]
        public string CategoryName { get; set; } = default!;
    }
}