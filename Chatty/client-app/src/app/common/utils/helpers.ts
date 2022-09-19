import { toast } from "react-toastify";


export const showErrors = (data: any) => {
    if (!data.errors) return;
    const errors = [];
    for (const key in data.errors) {
        if (data.errors[key]){
            errors.push(data.errors[key]);
        }
    }
    errors.flat().forEach(error => toast.error(error));
}