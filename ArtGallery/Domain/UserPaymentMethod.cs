using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserPaymentMethod: DomainEntityBaseMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}