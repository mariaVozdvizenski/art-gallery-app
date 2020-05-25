using System;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid InvoiceId { get; set; } = default!;
        public Invoice? Invoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}