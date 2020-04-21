using System;
using Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Payment : Payment<Guid>, IDomainBaseEntity
    {
    }

    public class Payment<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey InvoiceId { get; set; } = default!;
        
        public Invoice? Invoice { get; set; }
        
        public DateTime PaymentDate { get; set; }
        
        public decimal PaymentAmount { get; set; }
    }
}