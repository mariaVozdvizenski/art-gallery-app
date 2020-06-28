﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class DALPaintingView    
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
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;

        public int Quantity { get; set; }
        
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = default!;

        public ICollection<DALCommentView>? Comments { get; set; }
        public ICollection<PaintingCategory>? PaintingCategories { get; set; }
    }
}