import APP_CONSTANTS from "./ApplicationConstants";
import { RequestResult, SuccessApiCall } from "./Interfaces";

export async function fetchData<T>(endpoint: string) {
    return await fetch(endpoint)
        .then(data => data.json)
        .then(jsObject => {

            let response = (jsObject as unknown as SuccessApiCall<T>);

            switch (response.status) {
                case APP_CONSTANTS.status_success:
                    return new RequestResult<T>(response.data, null);

                case APP_CONSTANTS.status_error:
                    return new RequestResult<T>(null, response.message);

                default:
                    throw new Error("Not Implemented Exception");
            }
        })
        .catch(err => {
            console.error(err);
            return new RequestResult<T>(null, err.message);
        });
}
