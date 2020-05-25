using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        [MaxLength(4096)]
        public string? OrderDetails { get; set; }
        public string OrderStatusCode { get; set; } = default!;
    }
}