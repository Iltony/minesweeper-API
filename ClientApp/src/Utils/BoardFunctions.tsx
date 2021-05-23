import { Board, CellCoordinates, GameReducerState, InitializeAction, RequestResult, SuccessApiCall, User } from "./Interfaces";
import APP_CONSTANTS from "./ApplicationConstants";
import { postData } from "./PostData";

export const initializeAsync = async (state:GameReducerState, clickedCell:CellCoordinates) =>  {
    const endpoint = APP_CONSTANTS.initialize_url

    const userName = state.user != undefined ? state.user.username : null;

    const requestData:InitializeAction  = {
        username: userName,
        columns: state.columns,
        rows: state.rows,
        mines: state.mines,
        initialClickCell: {...clickedCell}
    }

    return await postData<InitializeAction, Board>(endpoint, requestData);
}