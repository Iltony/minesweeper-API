import { createContext, useState } from 'react';
import { GameConfigurationContextProps, GameConfigurationState } from '../Utils/Interfaces'
import { getDefaultBirthDate } from '../Utils/UserFunctions';

//General settings of the game, just a sample
const MIN_AGE_TO_REGISTER:Number = 10;

const initialState:GameConfigurationState = {
    allowAnomymusUser: true,
    minimumAllowedAge: MIN_AGE_TO_REGISTER,
    maxAllowedBirthDate:getDefaultBirthDate(MIN_AGE_TO_REGISTER)
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

