using System;

namespace PublicApi.DTO.v1
{
    public class InvoiceCreate
    {
        public Guid Id { get; set; } = default!;
        
        public int InvoiceNumber { get; set; }
        
        public DateTime InvoiceDate { get; set; }

        public string InvoiceDetails { get; set; } = default!;
        
        public Guid OrderId { get; set; } = default!;
        
        public Guid InvoiceStatusCodeId { get; set; } = default!;
        
        public string TelephoneNumber { get; set; } = default!;
        
        public string Country { get; set; } = default!;
        
        public string City { get; set; } = default!;
        
        public string Address { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;
    }
}