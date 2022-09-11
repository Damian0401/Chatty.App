import { MessageDto } from "./message";
import { UserDto } from "./user";

export interface RoomDto {
    id: string;
    name: string;
    messages: MessageDto[];
    users: UserDto[];
}