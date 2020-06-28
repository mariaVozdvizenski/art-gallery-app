using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Domain.Base;


namespace Domain.App
{
    public class Category : DomainEntityIdMetadata
    {
        [MaxLength(36)] 
        [MinLength(1)] 
        public string CategoryName { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}