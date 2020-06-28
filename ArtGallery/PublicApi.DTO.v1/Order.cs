using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }

        public Guid OrderStatusCodeId { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        
        public Guid AddressId { get; set; }
        
    }
}