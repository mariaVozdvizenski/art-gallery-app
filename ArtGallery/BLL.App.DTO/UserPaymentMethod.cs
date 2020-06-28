using System;
using BLL.App.DTO.Identity;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserPaymentMethod : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}