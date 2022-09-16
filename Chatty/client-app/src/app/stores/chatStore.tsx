import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { makeAutoObservable } from "mobx";
import { BASE_URL } from "../common/utils/constants";
import { Message } from "../models/message";
import { AddToRoomResponse, Room } from "../models/room";
import { store } from "./store";



export default class ChatStore {
    hubConnection: HubConnection | null = null;
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
        this.hubConnection.on('AddRoom', (room: Room) => this.addRoom(room));
        this.hubConnection.on('HandleErrors', (errors) => console.log(errors));
        this.hubConnection.on('ConnectToChat', (rooms: Room[]) => rooms.forEach(room => this.addRoom(room)));
        this.hubConnection.on('RecieveMessage', (message: Message) => this.addMessage(message));
        this.hubConnection.on('GetRoomDetails', (room: Room) => this.addRoom(room));
    }

    stopHubConnection = () => {
        this.hubConnection?.stop()
            .catch(error => console.log('Error stopping the connection: ', error));
    }

    private addRoom = (room: Room) => {
        this.roomRegistry.set(room.id, room);
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
        if (!room) return;

        room.messages?.push(message);
    }
}