﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class OrderStatusCode : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
       
        public string OrderStatusDescription { get; set; } = default!;

        public string Code { get; set; } = default!;

        public ICollection<Order>? Orders { get; set; }
    }
}