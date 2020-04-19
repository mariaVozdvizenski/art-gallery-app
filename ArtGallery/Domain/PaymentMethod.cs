using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class PaymentMethod : PaymentMethod<Guid>, IDomainEntityBaseMetadata
    {
        
    }
    public class PaymentMethod<TKey>: DomainEntityBaseMetadata<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MaxLength(128)]
        [MinLength(1)]
        public string PaymentMethodDescription { get; set; } = default!;

        [MaxLength(36)]
        [MinLength(1)]
        public string PaymentMethodCode { get; set; } = default!;

        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}