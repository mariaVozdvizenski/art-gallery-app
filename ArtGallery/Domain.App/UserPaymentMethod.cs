using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserPaymentMethod : DomainEntityIdMetadataUser<AppUser>
    {
        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}