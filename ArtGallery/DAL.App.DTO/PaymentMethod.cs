using System;
using System.Collections.Generic;
using ee.itcollege.mavozd.Contracts.Domain;

namespace DAL.App.DTO
{
    public class PaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string PaymentMethodDescription { get; set; } = default!;
        
        public string PaymentMethodCode { get; set; } = default!;

        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}