using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Category: DomainEntity
    {
        [MaxLength(36)] 
        [MinLength(1)] 
        public string Name { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}