using System;

namespace PublicApi.DTO.v1
{
    public class BasketDTO
    {
        public Guid Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public int ItemCount { get; set; }
    }
}