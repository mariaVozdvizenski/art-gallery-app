using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Basket : IDomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public DateTime DateCreated { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }

        public Guid Id { get; set; }
    }
}