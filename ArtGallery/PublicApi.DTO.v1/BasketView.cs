using System;

namespace PublicApi.DTO.v1
{
    public class BasketView
    {
        public Guid AppUserId { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public Guid Id { get; set; }

        public string UserName { get; set; } = default!;
    }
}