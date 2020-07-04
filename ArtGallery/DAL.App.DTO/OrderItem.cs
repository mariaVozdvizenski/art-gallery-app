using System;
using System.Collections.Generic;
using ee.itcollege.mavozd.Contracts.Domain;

namespace DAL.App.DTO
{
    public class OrderItem : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}