using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ArtistEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public string FirstName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public string LastName { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public string Country { get; set; } = default!;
        
    }
}