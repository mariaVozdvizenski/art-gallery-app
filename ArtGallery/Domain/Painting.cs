using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Painting: DomainEntity
    {
        [MaxLength(4096)] 
        [MinLength(1)] 
        public string Description { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        [MaxLength(36)] 
        [MinLength(1)] 
        public string Title { get; set; } = default!;
        
        [MaxLength(16)] 
        [MinLength(1)]  
        public string Size { get; set; } = default!;
        
        public Guid ArtistId { get; set; } = default!;
        public Artist? Artist { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PaintingCategory>? PaintingCategories { get; set; }
        public ICollection<BasketItem>? ItemBaskets { get; set; }
        public ICollection <OrderItem>? ItemOrders { get; set; }
    }
}