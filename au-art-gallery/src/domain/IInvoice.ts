import { IInvoiceStatusCode } from "./IInvoiceStatusCode";

export interface IInvoice {
    id: string;
    invoiceNumber: Number;
    invoiceDate: Date;
    invoiceDetails: string;
    orderId: string;
    invoiceStatusCodeId: string;
    invoiceStatusCode: IInvoiceStatusCode;
}