using System;
using System.Collections.Generic;
using ee.itcollege.mavozd.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Basket : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}