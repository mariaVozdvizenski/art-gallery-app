using System;

namespace PublicApi.DTO.v1
{
    public class UserPaymentMethod
    {
        public Guid Id { get; set; }
        public string UserPaymentMethodName { get; set; } = default!;
    }
}