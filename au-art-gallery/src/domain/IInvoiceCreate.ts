export interface IInvoiceCreate {
    invoiceNumber: Number;
    invoiceDate: Date;
    invoiceDetails: string;
    orderId: string;
    invoiceStatusCodeId: string;
    telephoneNumber: string;
    country: string;
    city: string;
    address: string;
    firstName: string;
    lastName: string;
}