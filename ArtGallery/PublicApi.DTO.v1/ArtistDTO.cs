using System;

namespace PublicApi.DTO.v1
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public int PaintingCount { get; set; }
    }
}