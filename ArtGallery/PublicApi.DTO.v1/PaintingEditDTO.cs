using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PaintingEditDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(36)] public string Title { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;
        
        public Guid ArtistId { get; set; } 
    }
}