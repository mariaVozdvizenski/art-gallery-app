using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Category : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(36)] 
        [MinLength(1)] 
        public string CategoryName { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}