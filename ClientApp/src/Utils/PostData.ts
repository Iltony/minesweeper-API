import APP_CONSTANTS from "./ApplicationConstants";
import { RequestResult, SuccessApiCall } from "./Interfaces";

export async function postData<T, K>(endpoint: string, data:T) {

    const postOptions ={
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        headers: {
            'Content-Type': 'application/json'
        },  
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    }

    return await fetch(endpoint, postOptions)

        .then(data => data.json)

        .then(jsObject => {

            let response = (jsObject as unknown as SuccessApiCall<K>);

            switch (response.status) {
                case APP_CONSTANTS.status_success:
                    return new RequestResult<K>(response.data, null);

                case APP_CONSTANTS.status_error:
                    return new RequestResult<K>(null, response.message);

                default:
                    throw new Error("Not Implemented Exception");
            }
        })
        .catch(err => {
            console.error(err);
            return new RequestResult<K>(null, err.message);
        });
}
