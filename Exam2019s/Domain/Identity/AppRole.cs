using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        [MinLength(1)] [MaxLength(256)] 
        public string DisplayName { get; set; } = default!;
    }
}