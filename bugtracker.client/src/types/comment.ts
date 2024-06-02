export interface IComment {
    id: string;
    content: string;
    createdAt: Date;
    updatedAt: Date;
    ticketId: string;
    userName: string;
}
