using System;

namespace PublicApi.DTO.v1
{
    public class Basket 
    {
        public Guid AppUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid Id { get; set; }
    }
}