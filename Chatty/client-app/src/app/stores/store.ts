import { createContext, useContext } from "react";
import ChatStore from "./chatStore";
import CommonStore from "./commonStore";
import UserStore from "./userStore";


interface Store {
    userStore: UserStore;
    commonStore: CommonStore;
    chatStore: ChatStore;
}

export const store: Store = {
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    chatStore: new ChatStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}