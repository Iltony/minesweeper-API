import {ReactNode } from "react";


export interface User {
    username: string,
    firstName?: string,
    lastName?: string,
    birthdate?: Date,
    millisecondsRecord?: Number
}

export interface Cell {
    column: Number,
    row: Number,
    position: Number,
    status: Number,
    itIsAMine: true,
    closerMinesNumber: Number
}

export interface CellCoordinates {
    Column: Number,
    Row: Number
}

export interface Board {
        id: string,
        owner: User,
        name: string,
        columns: Number,
        rows: Number,
        milliseconds: Number,
        cells: Cell[],
        gameStatus: number
}

//Game Configuration Context State
export interface GameConfigurationContextProps {
    children: ReactNode
}

export interface GameReducerState {
    user?: User,
    activeBoard?: Board
    columns: Number,
    rows: Number,
    mines:Number
}

export interface GameConfigurationState {
    allowAnomymusUser: Boolean,
    minimumAllowedAge: Number
}

// Fetch -------------------------------------------------------------------
export interface ApiCallResponse {
    status: string,
    message: string
}

export interface SuccessApiCall<T> extends ApiCallResponse {
    data: T
}

export interface ErrorApiCall extends ApiCallResponse {
}

export interface IRequestResult<T> {
    data: T | null,
    error: string | null
}


export class RequestResult<T> implements IRequestResult<T> {
    data: T | null;
    error: string | null;
    
     constructor(data:T | null, error: string | null) {
        this.data= data
        this.error= error
    }
}


// Components --------------------------------------------------------------
export interface ErrorProps {}
export interface HomeProps {}

export interface SavedBoardsProps {
    username: string
}

export interface SavedRowProps {
    board: Board,
    onSelectBoard: ()=>{}
}

export interface CellProps {
    Cell: Cell
}

export interface CellProps {
    Cell: Cell
}

export interface NewGameProps {}
export interface GameProps {}
export interface RegisterProps {}


export interface RowProps {
    onCellMainClick: ()=>{}
    onCellAlternativeClick: ()=>{}
    Cells: Cell[]
}
export interface CellProps {
    onCellMainClick: ()=>{}
    onCellAlternativeClick: ()=>{}
    Cell: Cell
}