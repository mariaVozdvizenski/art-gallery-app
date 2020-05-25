using System;
using Domain.Base;

namespace Domain.App
{
    public class Payment : DomainEntityIdMetadata
    {
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}