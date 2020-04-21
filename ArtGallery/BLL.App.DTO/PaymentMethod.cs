using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class PaymentMethod : PaymentMethod<Guid>, IDomainBaseEntity
    {
    }

    public class PaymentMethod<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string PaymentMethodDescription { get; set; } = default!;
        
        public string PaymentMethodCode { get; set; } = default!;

        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}