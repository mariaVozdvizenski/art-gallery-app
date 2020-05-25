﻿using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class ShipmentItem : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public Guid OrderItemId { get; set; } = default!;
        public OrderItem? OrderItem { get; set; }
        
        public Guid ShipmentId { get; set; } = default!;
        public Shipment? Shipment { get; set; }
    }
}