using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AdminOrderView
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }

        public Guid OrderStatusCodeId { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string OrderStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string OrderStatusCode { get; set; } = default!;

        public string UserName { get; set; } = default!;
        
        public ICollection<OrderItemView>? OrderItems { get; set; }
    }
}