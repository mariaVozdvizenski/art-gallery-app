using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Category : Category<Guid>, IDomainBaseEntity
    {
    }

    public class Category<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public string CategoryName { get; set; } = default!;

        public ICollection<PaintingCategory>? CategoryPaintings { get; set; }
    }
}