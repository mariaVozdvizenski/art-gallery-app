﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class InvoiceStatusCode: DomainEntityBaseMetadata
    {
        [MaxLength(128)]
        [MinLength(1)]
        public string InvoiceStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string Code { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}