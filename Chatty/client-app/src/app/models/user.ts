
export interface User {
    id: string;
    displayName: string;
    isAdministrator: boolean;
}

export interface UserProfile {
    userName: string;
    token: string;
}

export interface UserLoginValues {
    email: string;
    password: string;
}

export interface UserRegisterValues {
    email: string;
    password: string;
    userName: string;
}
