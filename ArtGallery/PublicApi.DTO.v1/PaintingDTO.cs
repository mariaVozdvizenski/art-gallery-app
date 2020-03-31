using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace PublicApi.DTO.v1
{
    public class PaintingDTO
    {
        public Guid Id { get; set; }

        [MinLength(1)] [MaxLength(36)] public string Title { get; set; } = default!;
        
        [MaxLength(4096)] [MinLength(1)] public string Description { get; set; } = default!;
        
        public decimal Price { get; set; }
    }
}