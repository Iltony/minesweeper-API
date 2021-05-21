import {ReactNode } from "react";


export interface User {
    username: string,
    firstName: string,
    lastName: string,
    birthdate: Date,
    millisecondsRecord: 0
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
}

export interface GameConfigurationState {
    allowAnomymusUser: Boolean,
}

// Fetch -------------------------------------------------------------------
export interface ApiCallResponse {
    status: string,
    message: string
}

export interface SuccessApiCall<T> extends ApiCallResponse {
    data: T ,
}

export interface ErrorApiCall<T> extends ApiCallResponse {
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