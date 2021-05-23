import { Typography } from "@material-ui/core";
import { FC, useReducer, useState } from "react"
import gameReducer, { gameInitialState } from "../Utils/GameReducer";
import { GameProps } from "../Utils/Interfaces";

import DummyBoardComponent from "./DummyBoardComponent";
import BoardComponent from "./BoardComponent";


const GameComponent:FC<GameProps> = (props: GameProps) =>
{
   const [state, dispatch] = useReducer(gameReducer, gameInitialState);
   const [message, setMessage] = useState("");
   const [hasError, setHasError] = useState(false);


   const setError = (value:boolean) => {
      setHasError(value);
   }
   const showMessage = (value:string) => {
      setMessage(value);
   }


   const CurrentBoardComponent = () => (state.activeBoard ?
      <BoardComponent setHasError={setError} setMessage={showMessage} ></BoardComponent> :
      <DummyBoardComponent  setHasError={setError} setMessage={showMessage}></DummyBoardComponent> )

   return (

      <div className={'board'}>
         <CurrentBoardComponent></CurrentBoardComponent>
         {message &&
               <Typography className={hasError ? 'error' : 'welcome'} variant="h6">{message}</Typography>
         }
      </div>
   )

}

export default GameComponent;
  
