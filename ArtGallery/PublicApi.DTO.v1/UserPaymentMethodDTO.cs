using System;

namespace PublicApi.DTO.v1
{
    public class UserPaymentMethodDTO
    {
        public Guid Id { get; set; }
        public string UserPaymentMethod { get; set; }
    }
}