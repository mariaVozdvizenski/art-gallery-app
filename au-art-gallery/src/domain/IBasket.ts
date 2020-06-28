import { IBasketItem } from "./IBasketItem";

export interface IBasket {
    id: string;
    dateCreated: string;
    appUserId: string;
    userName: string;
    basketItems: IBasketItem[]
}