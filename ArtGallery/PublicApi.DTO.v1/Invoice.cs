using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Invoice
    {
        public Guid Id { get; set; } = default!;
        
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDetails { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        
        public Guid InvoiceStatusCodeId { get; set; } = default!;
    }
}