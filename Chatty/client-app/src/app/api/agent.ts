import axios, { AxiosError, AxiosResponse } from "axios";
import { BASE_URL } from "../common/constants";


axios.defaults.baseURL = BASE_URL;

axios.interceptors.response.use(async response => response, (error: AxiosError) => {console.log(error);})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}