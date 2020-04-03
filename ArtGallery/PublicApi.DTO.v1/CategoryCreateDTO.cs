using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CategoryCreateDTO
    {
        [MaxLength(36)] 
        [MinLength(1)] 
        public string CategoryName { get; set; } = default!;
    }
}