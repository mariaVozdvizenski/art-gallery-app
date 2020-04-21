using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class InvoiceStatusCode : InvoiceStatusCode<Guid>, IDomainBaseEntity
    {
    }

    public class InvoiceStatusCode<TKey> : IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public string InvoiceStatusDescription { get; set; } = default!;

        public string Code { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}