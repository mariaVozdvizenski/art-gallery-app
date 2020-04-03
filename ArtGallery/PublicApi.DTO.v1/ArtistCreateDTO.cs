using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ArtistCreateDTO
    {
        [MaxLength(128)] 
        [MinLength(1)] 
        public string FirstName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public string LastName { get; set; } = default!;
        
        [MaxLength(4096)] 
        [MinLength(1)] 
        public string Bio { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public string PlaceOfBirth { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public string Country { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
    }
}