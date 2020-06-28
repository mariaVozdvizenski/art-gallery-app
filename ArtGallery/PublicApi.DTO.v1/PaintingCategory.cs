using System;

namespace PublicApi.DTO.v1
{
    public class PaintingCategory
    {
        public Guid Id { get; set; }

        public Guid PaintingId { get; set; }

        public Guid CategoryId { get; set; }
    }
}