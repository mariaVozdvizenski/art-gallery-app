using System;
using Contracts.DAL.Base;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserPaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}