import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import { BASE_CHAT_URL } from "../common/utils/constants";
import { showErrors } from "../common/utils/helpers";
import { Message, MessageSendValues } from "../models/message";
import { AddToRoomResponse, Room } from "../models/room";
import { User } from "../models/user";
import { store } from "./store";


export default class ChatStore {
    hubConnection: HubConnection | null = null;
    selectedRoom: Room | undefined = undefined;
    roomRegistry = new Map<string, Room>();

    constructor() {
        makeAutoObservable(this);
    }

    createHubConnection = () => {
        if (!store.commonStore.token) return;

        this.hubConnection = new HubConnectionBuilder()
            .withUrl(BASE_CHAT_URL, {
                accessTokenFactory: () => store.commonStore.token!
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .catch(error => console.log('Error establishing the connection: ', error));

        this.hubConnection.on('AddToRoom', (response: AddToRoomResponse) => this.addToRoom(response));
        this.hubConnection.on('AddRoom', (room: Room) => this.addRoom(room));
        this.hubConnection.on('HandleErrors', (errors) => showErrors(errors));
        this.hubConnection.on('ConnectToChat', (rooms: Room[]) => rooms.forEach(room => this.setRoom(room)));
        this.hubConnection.on('RecieveMessage', (message: Message) => this.addMessage(message));
        this.hubConnection.on('GetRoomDetails', (room: Room) => this.setRoom(room));
    }

    stopHubConnection = () => {
        this.roomRegistry.clear();
        this.selectedRoom = undefined;
        this.hubConnection?.stop()
            .catch(error => console.log('Error stopping the connection: ', error));
    }

    selectRoom = (id: string | undefined) => {
        if (!id) {
            history.push('/chat/notfound');
            return;
        }

        this.selectedRoom = this.roomRegistry.get(id);

        if (!this.selectedRoom) {
            history.push('/chat/notfound');
            return;
        }

        if (!this.selectedRoom.users) {
            this.hubConnection?.invoke('RoomDetails', this.selectedRoom.id);
        }
    }

    joinRoom = (roomId: string) => {
        this.hubConnection?.invoke('JoinRoom', roomId);
    }
    
    createRoom = (roomName: string) => {
        this.hubConnection?.invoke('CreateRoom', roomName);
    }

    sendMessage = (body: string) => {
        if (!this.selectedRoom || !this.hubConnection) return;

        let message: MessageSendValues = {
            roomId: this.selectedRoom.id,
            body: body
        };

        this.hubConnection.invoke('SendMessage', message);
    }
    
    deleteMessage = (messageId: string) => {
        this.hubConnection?.invoke('DeleteMessage', messageId);
    }

    get isRoomAdministrator() {
        return this.selectedRoom?.users?.some(u =>
            u.isAdministrator && u.id === store.userStore.user?.id);
    }

    private addRoom = (room: Room) => {
        this.setRoom(room);
        history.push(`/chat/${room.id}`)
    }

    private setRoom = (room: Room) => {
        room.messages?.forEach((message: Message) => {
            message.createdAt = new Date(message.createdAt);
        });
        runInAction(() => this.roomRegistry.set(room.id, room));

        if (this.selectedRoom?.id === room.id) {
            runInAction(() => this.selectedRoom = room);
        }
    }

    private getRoom = (id: string) => {
        return this.roomRegistry.get(id);
    }

    private addToRoom = (response: AddToRoomResponse) => {
        this.addMessage(response.message);
        this.addUser(response.user, response.id);
    }

    private addMessage = (message: Message) => {
        const room = this.getRoom(message.roomId);
        if (!room) return;
        if (!room.messages) room.messages = [];
        
        message.createdAt = new Date(message.createdAt);
        let messageIndex = room.messages.findIndex(x => x.id === message.id);

        if (messageIndex === -1) {
            room.messages.unshift(message);
        } else {
            room.messages[messageIndex] = message;
        }

        if (room.id === this.selectedRoom?.id) {
            this.selectedRoom = room;
        }
    }

    private addUser = (user: User, roomId: string) => {
        const room = this.getRoom(roomId);
        if (!room) return;
        if (!room.users) room.users = [];

        let userIndex = room.users.findIndex(x => x.id === user.id);

        if (userIndex === -1) {
            room.users.unshift(user);
        } else {
            room.users[userIndex] = user;
        }

        if (room.id === this.selectedRoom?.id) {
            this.selectedRoom = room;
        }
    }
}