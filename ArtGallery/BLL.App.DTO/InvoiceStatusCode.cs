using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class InvoiceStatusCode : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)]
        [MinLength(1)]
        public string InvoiceStatusDescription { get; set; } = default!;

        [MaxLength(128)]
        [MinLength(1)]
        public string Code { get; set; } = default!;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}