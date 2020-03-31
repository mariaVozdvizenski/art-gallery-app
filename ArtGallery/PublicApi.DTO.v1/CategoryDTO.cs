using System;

namespace PublicApi.DTO.v1
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        
        public string CategoryName { get; set; }
        
        public int PaintingCount { get; set; }
    }
}