﻿using System;
using Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }

}