using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class OrderStatusCode : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public string OrderStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string Code { get; set; } = default!;

        public ICollection<Order>? Orders { get; set; }
    }
}