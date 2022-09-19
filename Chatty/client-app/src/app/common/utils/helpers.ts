import { toast } from "react-toastify";


export const showErrors = (data: any) => {
    if (!data.errors) return;
    for (const key in data.errors) {
        if (data.errors[key]){
            toast.error(data.errors[key]);
        }
    }
}