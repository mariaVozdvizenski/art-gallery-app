using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class JwtResponseDTO
    {
        [Required]
        public string Token { get; set; } = default!;
        [Required]
        public string Status { get; set; } = default!;
        
        [Required]
        public string UserName { get; set; } = default!;
        
        [Required]
        public ICollection<string> UserRoles { get; set; } = default!;

        public Guid AppUserId { get; set; }

    }
}