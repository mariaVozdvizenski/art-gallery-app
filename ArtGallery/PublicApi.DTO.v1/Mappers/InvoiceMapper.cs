namespace PublicApi.DTO.v1.Mappers
{
    public class InvoiceMapper : ApiBaseMapper<BLL.App.DTO.Invoice, Invoice>
    {
        InvoiceStatusCodeMapper _invoiceStatusCodeMapper = new InvoiceStatusCodeMapper();
        
        public BLL.App.DTO.Invoice MapFromInvoiceCreate(InvoiceCreate inObject)
        {
            return new BLL.App.DTO.Invoice()
            {
                InvoiceDate = inObject.InvoiceDate,
                Id = inObject.Id,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                OrderId = inObject.OrderId
            };
        }

        public InvoiceView MapForInvoiceView(BLL.App.DTO.Invoice inObject)
        {
            return new InvoiceView()
            {
                Id = inObject.Id,
                InvoiceDate = inObject.InvoiceDate,
                InvoiceDetails = inObject.InvoiceDetails,
                InvoiceNumber = inObject.InvoiceNumber,
                InvoiceStatusCode = _invoiceStatusCodeMapper.Map(inObject.InvoiceStatusCode!),
                InvoiceStatusCodeId = inObject.InvoiceStatusCodeId,
                OrderId = inObject.OrderId
            };
        }
    }
}