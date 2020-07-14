using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.Identity
{
    public class RegisterDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; } = default!;
    }
}