using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;
using Contracts.Domain;

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