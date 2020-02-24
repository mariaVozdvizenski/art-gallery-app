﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser: IdentityUser
    {
        [MaxLength(36)] 
        public override string Id { get; set; } = default!;

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
        public ICollection<Basket>? Baskets { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
    
}