export interface Ticket {
    id: string;
    title: string;
    description: string;
    priority: string;
    status: number;
    assignedUserNames: string[];
    createdAt: Date;
    updatedAt: Date;
    tags: string[];
}

export interface TicketDisplay {
    id: string;
    title: string;
}
