import { IComment } from "./IComment";
import { IPaintingCategory } from "./IPaintingCategory";

export interface IPainting {
    id: string;
    title: string;
    price: number;
    size: string;
    imageName: string;
    description: string;
    artistName: string;
    artistId: string;
    comments: IComment[];
    paintingCategories: IPaintingCategory[];
    quantity: number;
    imageUrl?: string;
}