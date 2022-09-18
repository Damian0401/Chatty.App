import { makeAutoObservable, runInAction } from "mobx";


export default class ModalStore {
    isOpen: boolean = false;
    content: JSX.Element | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    openModal = (content: JSX.Element) => {
        runInAction(() => {
            this.content = content;
            this.isOpen = true;
        })
    }

    closeModal = () => {
        runInAction(() => {
            this.isOpen = false;
            this.content = null;
        })
    }
}