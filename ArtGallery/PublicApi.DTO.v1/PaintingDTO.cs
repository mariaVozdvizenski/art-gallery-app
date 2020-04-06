using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace PublicApi.DTO.v1
{
    public class PaintingDTO
    {
        public Guid Id { get; set; }

        [MaxLength(4096)] 
        [MinLength(1)] 
        
        public decimal Price { get; set; }
        
        [MaxLength(36)] 
        [MinLength(1)] 
        public string Title { get; set; } = default!;
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;
        
        public Guid ArtistId { get; set; } 
        public ArtistDTO Artist { get; set; } = default!;
    }
}