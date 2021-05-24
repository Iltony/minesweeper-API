import React from "react"
import { createContext, useReducer } from "react"
import gameReducer, { gameInitialState } from "./GameReducer"
import { GameProviderProps, GameReducerState } from "./Interfaces"

const GameStateContext = createContext(null as unknown as GameReducerState)
const GameDispatchContext = createContext(null as any)

export const GameProvider = (props:GameProviderProps) => {
    const [gameState, gameDispatch] = useReducer(gameReducer, gameInitialState);

    return (
        <GameDispatchContext.Provider value={gameDispatch}>
            <GameStateContext.Provider value={gameState}>
                {props.children}
            </GameStateContext.Provider>
        </GameDispatchContext.Provider>
    );
}


export const useGameState = () => {
    const context = React.useContext(GameStateContext)

    if (!context){
        throw new Error('GameStateContext must be used within a GameProvider')
    }

    return context;
}


export const useGameDispatch = () => {
    const context = React.useContext(GameDispatchContext)

    if (!context){
        throw new Error('GameStateContext must be used within a GameProvider')
    }

    return context;
}