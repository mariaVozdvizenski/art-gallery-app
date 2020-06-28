using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.mavozd.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Artist : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string FirstName { get; set; } = default!;
        
        [MaxLength(128)] 
        [MinLength(1)] 
        public  string LastName { get; set; } = default!;
        
        [MaxLength(4096)] 
        [MinLength(1)] 
        public  string Bio { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public  string PlaceOfBirth { get; set; } = default!;

        [MaxLength(128)] 
        [MinLength(1)] 
        public  string Country { get; set; } = default!;
        
        public  DateTime DateOfBirth { get; set; } = default!;
        
        public  ICollection<Painting>? Paintings { get; set; }

        public  string FirstLastName => FirstName + " " + LastName;
    }
}