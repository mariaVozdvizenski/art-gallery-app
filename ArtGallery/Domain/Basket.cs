﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Basket: DomainEntity
    {
        public DateTime DateCreated { get; set; }

        [MaxLength(36)] 
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}