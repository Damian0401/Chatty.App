import axios, { AxiosError, AxiosResponse } from "axios";
import { BASE_API_URL } from "../common/utils/constants";
import { showErrors } from "../common/utils/helpers";
import { UserLoginValues, UserProfile, UserRegisterValues } from "../models/user";
import { store } from "../stores/store";
import { history } from "../.."
import { toast } from "react-toastify";


axios.defaults.baseURL = BASE_API_URL;

axios.interceptors.request.use(request => {
    const token = store.commonStore.token;
    if (token) request.headers!.Authorization = `Bearer ${token}`;
    return request;
});

axios.interceptors.response.use(response => response, (error: AxiosError) => {
    const { data, status } = error.response!;
    switch (status) {
        case 0:
            toast.error('Network error');
        break;
        case 400:
            showErrors(data);
            break;
        case 401:
            store.userStore.logout();
            toast.error('Session expired - please login again');
            break;
        case 404:
            history.push('/chat/notfound');
            break;
    }
    return Promise.reject(error);
})

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