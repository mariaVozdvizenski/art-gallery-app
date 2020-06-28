
export interface IBasketItem {
    id: string;
    quantity: number;
    dateCreated: Date;
    basketId: string;
    paintingId: string;
    paintingTitle: string;
    paintingPrice: number;
    paintingSize : string;
    paintingQuantity: number;
    paintingImageUrl: string;
    imageUrl?: string;
}