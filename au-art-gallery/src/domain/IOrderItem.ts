export interface IOrderItem {
    id: string;
    paintingId: string;
    paintingTitle: string;
    imageName: string;
    paintingPrice: string;
    orderId: string;
    quantity: Number;
    imageUrl?: string;
}