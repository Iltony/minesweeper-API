import { FC, useEffect, useReducer, useState } from "react"
import gameReducer, { gameInitialState } from "../Utils/GameReducer";
import { Board, SavedBoardsProps } from "../Utils/Interfaces";


const SavedBoardComponent:FC<SavedBoardsProps> = (props: SavedBoardsProps) =>
{

   const [boardsList, setBoards] = useState<Board[]>([]);

   const [gameState, dispatcher] = useReducer(gameReducer, gameInitialState);

   useEffect(
      () => {

         let boards:Board[] = []



         
         // Fetch Saved boards and store in the state


         setBoards(boards)
      }
      , []);


   const onBoardSelected = (board: Board) => {  

         //*dispatchEvent
      

         window.location.replace("/play")
   }


   return (
         <h4 className="welcome">{`Present a list of boards`}</h4>
   );
}



export default SavedBoardComponent;