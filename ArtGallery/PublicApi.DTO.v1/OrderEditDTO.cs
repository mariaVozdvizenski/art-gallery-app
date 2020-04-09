using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class OrderEditDTO
    {
        public Guid Id { get; set; }

        [MaxLength(4096)] public string OrderDetails { get; set; } = default!;

        public Guid OrderStatusCodeId { get; set; } = default!;
    }
}