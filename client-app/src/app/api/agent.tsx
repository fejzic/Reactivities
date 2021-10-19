
import axios, { AxiosResponse } from 'axios'
import {Activity} from '../models/activity';

const sleep = (delay : number) =>{
    return new Promise((resolve) =>{
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL='http://localhost:5000/api';

axios.interceptors.response.use(async response =>{
    try {
        await sleep(1000);
        return response;
    }
    catch (error) {
        console.log(error);
        return Promise.reject(error);
    }
})

const responseBody =  (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string,body: {}) => axios.post(url,body).then(responseBody),
    put: (url: string,body: {}) => axios.put(url,body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const Activitie = {
    list: () => requests.get('/activities'),
    create: (activity: Activity) => axios.post<void>('/activities',activity),
    update: (activity : Activity) => axios.put<void>(`/activities/${activity.id}`,activity),
    delete : (id:string) => axios.delete<void>(`/activities/${id}`)

}

const agent = {
    Activitie
}

export default agent;