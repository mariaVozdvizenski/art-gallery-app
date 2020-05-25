import { IPainting } from "./IPainting";

export interface IArtist {
    id: string;
    firstName: string;
    lastName: string;
    country: string;
    paintings: IPainting[];
    dateOfBirth: string;
    placeOfBirth: string;
    bio: string;
}