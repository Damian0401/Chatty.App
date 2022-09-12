import { makeAutoObservable, reaction } from "mobx";


export default class CommonStore {
    token: string | null = window.localStorage.getItem('jwtToken');

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token,
            token => {
                if (token) {
                    window.localStorage.setItem('jwtToken', token);
                } else {
                    window.localStorage.removeItem('jwtToken');
                }
            }
        )
    }

    setToken(token: string | null) {
        this.token = token;
    }
}