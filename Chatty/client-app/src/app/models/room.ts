import { Message } from "./message";
import { User } from "./user";

export interface Room {
    id: string;
    name: string;
    messages: Message[] | null;
    users: User[] | null;
}

export interface AddToRoomResponse {
    id: string;
    user: User;
    message: Message;
}