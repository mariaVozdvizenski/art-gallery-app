using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ArtistView
    {
        public Guid Id { get; set; }

        [MinLength(1)] [MaxLength(128)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string Country { get; set; } = default!;
        
        public  DateTime DateOfBirth { get; set; } 
        
        [MaxLength(4096)] 
        [MinLength(1)] 
        public string Bio { get; set; } = default!;

        public ICollection<Painting>? Paintings { get; set; }
    }
}