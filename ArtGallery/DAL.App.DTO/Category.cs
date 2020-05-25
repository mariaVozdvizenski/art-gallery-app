using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Category : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        [MaxLength(36)] 
        [MinLength(1)] 
        public string CategoryName { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}