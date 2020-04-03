using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }

        [MinLength(1)] [MaxLength(64)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string LastName { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Country { get; set; } = default!;

        public int PaintingCount { get; set; }
    }
}