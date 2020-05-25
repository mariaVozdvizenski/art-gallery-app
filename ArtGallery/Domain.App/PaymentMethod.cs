using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class PaymentMethod : DomainEntityIdMetadata
    {
        [MaxLength(4096)]
        [MinLength(1)]
        public string PaymentMethodDescription { get; set; } = default!;

        [MaxLength(36)]
        [MinLength(1)]
        public string PaymentMethodCode { get; set; } = default!;

        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}