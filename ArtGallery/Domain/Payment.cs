using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Payment: DomainEntityEntityBaseMetadata
    {
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}