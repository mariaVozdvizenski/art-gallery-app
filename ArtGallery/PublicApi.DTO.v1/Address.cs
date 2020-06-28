using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Address
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        
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
    }
}