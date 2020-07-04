using System;

namespace PublicApi.DTO.v1
{
    public class InvoiceView
    {
        public Guid Id { get; set; } = default!;
        
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDetails { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;

        public Guid InvoiceStatusCodeId { get; set; } = default!;
        public InvoiceStatusCode? InvoiceStatusCode { get; set; }
    }
}