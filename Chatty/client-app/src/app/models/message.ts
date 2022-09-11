
export interface Message {
    id: string;
    body: string;
    isDeleted: boolean;
    createdAt: Date;
    authorId: string | null;
    roomId: string;
}

export interface MessageSendValues {
    roomId: string;
    body: string;
}