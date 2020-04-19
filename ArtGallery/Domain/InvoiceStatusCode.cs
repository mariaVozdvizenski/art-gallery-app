using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class InvoiceStatusCode : InvoiceStatusCode<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class InvoiceStatusCode<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
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