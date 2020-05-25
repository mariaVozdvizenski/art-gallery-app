using System;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        
    }
}