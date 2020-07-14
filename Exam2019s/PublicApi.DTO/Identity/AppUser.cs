using System;

namespace PublicApi.DTO.Identity
{
    public class AppUser 
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}