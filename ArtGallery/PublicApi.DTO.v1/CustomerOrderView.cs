﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CustomerOrderView
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }

        public Guid OrderStatusCodeId { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        
        public Guid AddressId { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string OrderStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string OrderStatusCode { get; set; } = default!;
    }
}