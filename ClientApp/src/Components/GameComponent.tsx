import { Typography } from "@material-ui/core";
import { FC, useState } from "react";
import { GameProps } from "../Utils/Interfaces";

import DummyBoardComponent from "./DummyBoardComponent";
import BoardComponent from "./BoardComponent";
import { useGameState } from "../Utils/GameContext";


const GameComponent:FC<GameProps> = (props: GameProps) =>
{
   const hasActiveBoard = useGameState().activeBoard !== undefined;

   const [message, setMessage] = useState("");
   const [hasError, setHasError] = useState(false);

   const setError = (value:boolean) => {
      setHasError(value);
   }
   const showMessage = (value:string) => {
      setMessage(value);
   }


   const CurrentBoardComponent = () => (hasActiveBoard ?
      <BoardComponent setHasError={setError} setMessage={showMessage} ></BoardComponent> :
      <DummyBoardComponent  setHasError={setError} setMessage={showMessage} ></DummyBoardComponent> )

   return (

      <div className={'board'}>
         <CurrentBoardComponent></CurrentBoardComponent>
         {message &&
               <Typography className={hasError ? 'error' : 'welcome'} variant="body2">{message}</Typography>
         }
      </div>
   )

}

export default GameComponent;
  
