﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class OrderItem : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public Guid PaintingId { get; set; } = default!;
        public Painting? Painting { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }

        public ICollection<ShipmentItem>? ItemShipments { get; set; }
    }
}