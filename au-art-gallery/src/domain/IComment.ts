export interface IComment {
    id: string;
    appUserId: string;
    commentBody: string;
    createdAt: Date;
    createdBy: string;
    paintingId: string;
}