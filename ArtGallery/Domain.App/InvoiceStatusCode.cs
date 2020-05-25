using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class InvoiceStatusCode : DomainEntityIdMetadata
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