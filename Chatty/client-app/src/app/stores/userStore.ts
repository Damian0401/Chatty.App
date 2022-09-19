import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { UserLoginValues, UserProfile, UserRegisterValues } from "../models/user";
import { store } from "./store";


export default class UserStore {
    user: UserProfile | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    login = async (values: UserLoginValues) => {
        const user = await agent.Account.login(values);
        store.commonStore.setToken(user.token);
        runInAction(() => this.user = user);
        history.push('/chat')
    }

    logout = () => {
        store.commonStore.setToken(null);
        runInAction(() => this.user = null);
        history.push('/');
    }

    register = async (values: UserRegisterValues) => {
        const user = await agent.Account.register(values);
        store.commonStore.setToken(user.token);
        runInAction(() => this.user = user);
        history.push('/chat')
    }

    getUser = async () => {
        const user = await agent.Account.current();
        store.commonStore.setToken(user.token);
        runInAction(() => this.user = user);
    }
}