import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import { BASE_URL } from "../common/utils/constants";
import { Message } from "../models/message";
import { AddToRoomResponse, Room } from "../models/room";
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
            .withUrl(BASE_URL + '/hubs/chat', {
                accessTokenFactory: () => store.commonStore.token!
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .catch(error => console.log('Error establishing the connection: ', error));

        this.hubConnection.on('AddToRoom', (response: AddToRoomResponse) => this.addToRoom(response));
        this.hubConnection.on('AddRoom', (room: Room) => this.setRoom(room));
        this.hubConnection.on('HandleErrors', (errors) => console.log(errors));
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

    get isRoomAdministrator() {
        return this.selectedRoom?.users?.some(u => 
            u.isAdministrator && u.id === store.userStore.user?.id);
    }

    private setRoom = (room: Room) => {
        room.messages?.forEach((message: Message) => {
            message.createdAt = new Date(message.createdAt + 'Z');
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
        const room = this.getRoom(response.id);
        if (!room) return;

        room.users?.push(response.user);
        room.messages?.push(response.message);
    }

    private addMessage = (message: Message) => {
        const room = this.getRoom(message.roomId);
        if (!room || !room.messages) return;

        message.createdAt = new Date(message.createdAt + 'Z');
        let messageId = room.messages.findIndex(x => x.id === message.id);

        if (messageId === -1) {
            room.messages.push(message);
        } else {
            room.messages[messageId] = message;
        }
    }
}