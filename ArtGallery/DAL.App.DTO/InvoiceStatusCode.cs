﻿using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class InvoiceStatusCode :  IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string InvoiceStatusDescription { get; set; } = default!;

        public string Code { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }
    }
    
}