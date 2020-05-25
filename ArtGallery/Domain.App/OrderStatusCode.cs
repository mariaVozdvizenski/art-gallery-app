using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class OrderStatusCode : DomainEntityIdMetadata
    {
        [MaxLength(128)] 
        [MinLength(1)] 
        public string OrderStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string Code { get; set; } = default!;

        public ICollection<Order>? Orders { get; set; }
    }
}