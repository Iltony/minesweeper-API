import { RequestResult, SuccessApiCall, User } from "./Interfaces";
import APP_CONSTANTS from "./ApplicationConstants";
import { fetchData } from "./FetchData";
import { postData } from "./PostData";



export const getUserAsync = 
    async (username: string) =>  {

        if (!username){
            return new RequestResult<User>(null, "Must specify a username")
        }

        const endpoint = APP_CONSTANTS.user_getuser_url + username
        
        return await fetchData(endpoint);
}



export const registerUserAsync = async (user: User, minimumAllowedAge: Number) =>  {
        const endpoint = APP_CONSTANTS.user_register_url
        let today = new Date()
        let validationDate = new Date(`${(today.getFullYear() - minimumAllowedAge.valueOf()).toString}-${today.getMonth().toString}-${today.getDay().toString}-` ) 

        if (!user.username && user.username.length > 1){
            return new RequestResult<User>(null, "Must specify a valid username")
        }

        if (!user.birthdate){
            return new RequestResult<User>(null, "Must specify a birthdate")
        }

        if (user.birthdate > validationDate  ){
            return new RequestResult<User>(null, "Must have more than 10 years to register")
        }

        return await postData(endpoint, user);
}