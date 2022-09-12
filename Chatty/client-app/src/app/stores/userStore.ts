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
        try {
            const user = await agent.Account.login(values);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/chat')
        } catch (error) {
            console.error(error);
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        runInAction(() => this.user = null);
        history.push('/');
    }

    register = async (values: UserRegisterValues) => {
        try {
            const user = await agent.Account.register(values);
            store.commonStore.setToken(user.token);
            runInAction(() => this.user = user);
            history.push('/chat')
        } catch (error) {
            console.error(error);
        }
    }
}