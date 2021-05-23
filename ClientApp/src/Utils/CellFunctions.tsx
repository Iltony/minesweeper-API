import { Board, CellAction, CellCoordinates, GameReducerState, RequestResult } from "./Interfaces";
import APP_CONSTANTS from "./ApplicationConstants";
import { postData } from "./PostData";

const performActionAsync = async (endpoint:string, state:GameReducerState, clickedCell:CellCoordinates) =>  {
        
        if (state.activeBoard){
                const requestData:CellAction  = {
                        board: {...state.activeBoard},
                        cell: {...clickedCell}
                }

                return await postData<CellAction, Board>(endpoint, requestData);
        }
        else {
                return new RequestResult<Board>(null, "Board is not Valid")
        }
}

export const checkAsync = async (state:GameReducerState, clickedCell:CellCoordinates) =>  
                                (performActionAsync(APP_CONSTANTS.cell_check_url, state, clickedCell))
export const flagAsync = async (state:GameReducerState, clickedCell:CellCoordinates) => 
                                (performActionAsync(APP_CONSTANTS.cell_flag_url, state, clickedCell))
