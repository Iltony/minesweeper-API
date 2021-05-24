import { GameReducerState } from "./Interfaces";

export const gameDefaults = {
	DEFAULT_VALUES: 10,
};

export const gameInitialState:GameReducerState = {
	user: undefined,
	activeBoard: undefined,
	columns: gameDefaults.DEFAULT_VALUES,
    rows: gameDefaults.DEFAULT_VALUES,
    mines: gameDefaults.DEFAULT_VALUES
};

export const gameReducerActions = {
	SET_ANONYMOUS: 'SET_ANONYMOUS',
	SET_USER: 'SET_USER',
	SET_BOARD: 'SET_BOARD',
	SET_GAME_SETTINGS: 'SET_GAME_SETTINGS',

	// SET_COLUMNS: 'SET_COLUMNS',
	// SET_ROWS: 'SET_ROWS',
	// SET_MINES: 'SET_MINES'
};

const gameReducer = (state: {}, action: any) => {


	switch (action.type) {

		case gameReducerActions.SET_ANONYMOUS:
			return { ...state, user: undefined, activeBoard: undefined } as GameReducerState;

		case gameReducerActions.SET_USER:

			if ((state as GameReducerState).activeBoard) {

				// set the user in the board
				return {
					...state, 
					user: { ...action.payload },
					activeBoard: { ...(state as GameReducerState).activeBoard, owner: { ...action.payload } }
				} as GameReducerState;
			};

			return { ...state, user: action.payload } as GameReducerState;

		case gameReducerActions.SET_BOARD:

			return { ...state, activeBoard: action.payload } as GameReducerState;

		// case gameReducerActions.SET_COLUMNS:
		// 	return { ...state, columns: action.payload } as GameReducerState;

		// case gameReducerActions.SET_ROWS:
		// 	return { ...state, rows: action.payload } as GameReducerState;
				
		// case gameReducerActions.SET_MINES:
		// 	return { ...state, mines: action.payload } as GameReducerState;


		case gameReducerActions.SET_GAME_SETTINGS:
		 	return { ...state, ...action.payload } as GameReducerState;




		default:
			return state as GameReducerState;
   
	}
}

export default gameReducer;