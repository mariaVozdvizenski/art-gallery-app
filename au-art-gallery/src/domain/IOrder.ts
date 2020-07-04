import { IAddress } from "./IAddress";
import { IOrderItem } from "./IOrderItem";
import { IInvoice } from "./IInvoice";

export interface IOrder{
    id?: string;
    orderDate: string;
    orderDetails?: string;
    orderStatusCodeId: string;
    appUserId: string;
    addressId: string;
    address: IAddress;
    orderStatusCodeDescription: string;
    orderStatusCode: string;
    userName: string;
    orderItems: IOrderItem[];
    total?: number;
    orderDateString?: string;
    invoice?: IInvoice;
}