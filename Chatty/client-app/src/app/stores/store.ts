import { createContext, useContext } from "react";
import ChatStore from "./chatStore";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import UserStore from "./userStore";


interface Store {
    userStore: UserStore;
    commonStore: CommonStore;
    chatStore: ChatStore;
    modalStore: ModalStore;
}

export const store: Store = {
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    chatStore: new ChatStore(),
    modalStore: new ModalStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}