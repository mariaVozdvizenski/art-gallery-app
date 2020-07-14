using System;
using System.ComponentModel.DataAnnotations;


namespace PublicApi.DTO.Identity
{
    public class AppRole 
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string DisplayName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = default!;
    }
}