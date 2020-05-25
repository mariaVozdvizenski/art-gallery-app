import { IComment } from "./IComment";

export interface IPainting {
    id: string;
    title: string;
    price: number;
    size: string;
    description: string;
    artistName: string;
    artistId: string;
    comments: IComment[];
    quantity: number;
}