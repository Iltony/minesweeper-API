import { ApiCallResponse, SuccessApiCall, User } from "./Interfaces";
import APP_CONSTANTS from "./ApplicationConstants";


export const getUserAsync = 
    async (username: string) =>  {

        if (!username){
            return null
        }

        const endpoint = APP_CONSTANTS.user_getuser_url + username

        return await fetch(endpoint)
                    .then(data => data.json)
                    .then(data => {
                        const status = (data as unknown as ApiCallResponse).status
                        switch (status) {
                            case APP_CONSTANTS.status_success:
                                return (data as unknown as SuccessApiCall<User>).data;

                            case APP_CONSTANTS.status_error:
                                return null;

                            default:
                                throw new Error("Not Implemented Exception")
                        }
                    })
                    .catch(err => {
                        console.error(err);
                        return null;
                    })
}