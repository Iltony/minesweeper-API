import { Board, GameReducerState, User } from "./Interfaces";

export const gameInitialState:GameReducerState = {
	user: undefined,
	activeBoard: undefined
};

export const gameReducerConstants = {
	SET_ANONYMOUS: 'SET_ANONYMOUS',
	SET_USER: 'SET_USER',
	SET_BOARD: 'SET_BOARD',
}


type CellType = {
    column: Number,
    row: Number,
    position: Number,
    status: Number,
    itIsAMine: Boolean,
    closerMinesNumber: Number
}

type UserType = {
    username: string,
    firstName: string,
    lastName: string,
    birthdate: Date,
    millisecondsRecord: 0
}

type BoardType = {
        id: string,
        owner: UserType,
        name: string,
        columns: Number,
        rows: Number,
        milliseconds: Number,
        cells: CellType[],
        gameStatus: number
}

const gameReducer = (state: {}, action: any) => {


	switch (action.type) {

		case gameReducerConstants.SET_ANONYMOUS:
			return { ...state, user: undefined, activeBoard: undefined }

		case gameReducerConstants.SET_USER:

			if ((state as GameReducerState).activeBoard) {

				// set the user in the board
				return {
					...state, 
					user: { ...action.payload },
					activeBoard: { ...(state as GameReducerState).activeBoard, owner: { ...action.payload } }
				}
			};

			return { ...state, user: action.payload }

		case gameReducerConstants.SET_BOARD:

			return { ...state, activeBoard: action.payload }

    		default:
	 		return state
	}
}

export default gameReducer;