using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Address : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string FirstName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string LastName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string ShippingAddress { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string Country { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string City { get; set; } = default!;
        
        public  int Zip { get; set; }

        public ICollection<Order>? AddressOrders { get; set; }
    }
}