﻿using System;
using System.Collections.Generic;
using ee.itcollege.mavozd.Domain.Base;


namespace Domain.App
{
    public class OrderItem : DomainEntityIdMetadata
    {
        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public int Quantity { get; set; }
    }
}