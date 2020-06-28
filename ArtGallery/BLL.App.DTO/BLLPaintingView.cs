using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Contracts.Domain;

namespace BLL.App.DTO
{
    public class BLLPaintingView 
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }
        
        public string ImageName { get; set; } = default!;

        [MaxLength(4096)] 
        [MinLength(1)] 
        public string Description { get; set; } = default!;
        
        [MaxLength(36)] 
        [MinLength(1)] 
        public string Title { get; set; } = default!;

        public int Quantity { get; set; }
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;
        
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = default!;

        public ICollection<BLLCommentView>? Comments { get; set; }
        public ICollection<PaintingCategory>? PaintingCategories { get; set; }
    }
}