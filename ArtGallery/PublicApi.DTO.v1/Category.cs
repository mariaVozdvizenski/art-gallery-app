using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Category
    {
        public Guid Id { get; set; }

        [MinLength(1)] [MaxLength(36)] public string CategoryName { get; set; } = default!;
        
        public int PaintingCount { get; set; }
    }
}