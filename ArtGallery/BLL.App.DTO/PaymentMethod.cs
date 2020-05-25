using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class PaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        public string PaymentMethodDescription { get; set; } = default!;

        [MaxLength(36)]
        [MinLength(1)]
        public string PaymentMethodCode { get; set; } = default!;

        public ICollection<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}