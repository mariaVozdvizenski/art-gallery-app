using System;

namespace PublicApi.DTO.v1
{
    public class CommentEditDTO
    {
        public Guid Id { get; set; }

        public string CommentBody { get; set; } = default!;
    }
}