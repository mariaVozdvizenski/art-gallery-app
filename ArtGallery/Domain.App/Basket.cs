using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Basket : DomainEntityIdMetadataUser<AppUser>
    {
        public DateTime DateCreated { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}