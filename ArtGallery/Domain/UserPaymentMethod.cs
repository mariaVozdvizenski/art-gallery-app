using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserPaymentMethod: DomainEntity
    {
        [MaxLength(36)] 
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)]
        public string PaymentMethodId { get; set; } = default!;
        public PaymentMethod? PaymentMethod { get; set; }
    }
}