using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Address : DomainEntityIdMetadataUser<AppUser>
    {
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