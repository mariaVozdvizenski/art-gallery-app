using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace PublicApi.DTO.v1
{
    public class PaintingCreateDTO
    {
        [MaxLength(4096)] 
        [MinLength(1)] 
        public string Description { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        [MaxLength(36)] 
        [MinLength(1)] 
        public string Title { get; set; } = default!;
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;
        
        public Guid ArtistId { get; set; } 
    }
}