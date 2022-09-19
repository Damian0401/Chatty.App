import axios, { AxiosError, AxiosResponse } from "axios";
import { BASE_API_URL } from "../common/utils/constants";
import { UserLoginValues, UserProfile, UserRegisterValues } from "../models/user";
import { store } from "../stores/store";


axios.defaults.baseURL = BASE_API_URL;

axios.interceptors.response.use(async response => response, (error: AxiosError) => {console.log(error);});

axios.interceptors.request.use(request => {
    const token = store.commonStore.token;
    if (token) request.headers!.Authorization = `Bearer ${token}`;
    return request;
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Account = {
    current: () => requests.get<UserProfile>('/account'),
    login: (user: UserLoginValues) => requests.post<UserProfile>('/account/login', user),
    register: (user: UserRegisterValues) => requests.post<UserProfile>('/account/register', user),
};

const agent = {
    Account,
};


export default agent;