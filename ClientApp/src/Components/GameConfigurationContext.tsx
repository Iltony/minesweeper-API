import { createContext, useState } from 'react';
import { GameConfigurationContextProps, GameConfigurationState } from '../Utils/Interfaces'

//General settings of the game, just a sample

const initialState:GameConfigurationState = {
    allowAnomymusUser: true,
    minimumAllowedAge: 10
}

export const GameConfigurationContext = createContext(initialState);

export const GameConfigurationConsumer = GameConfigurationContext.Consumer;

export const GameConfigurationProvider = (props: GameConfigurationContextProps) => {

    const [configState, setConfigurationState] = useState(initialState)

    return (
        <GameConfigurationContext.Provider value={configState}>
           {props.children}
        </GameConfigurationContext.Provider>
    )
}

