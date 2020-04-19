using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Payment : Payment<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class Payment<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}