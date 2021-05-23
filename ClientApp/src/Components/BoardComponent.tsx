import { FC, useReducer, useState } from "react"
import gameReducer, { gameInitialState, gameReducerActions } from "../Utils/GameReducer";
import { Board, GameReducerState, RequestResult } from "../Utils/Interfaces";
import { Cell, CellCoordinates } from "../Utils/Interfaces";
import { BoardProps } from "../Utils/Interfaces";
import { GameStatus } from "../Utils/CellConstants";
import RowComponent from "./RowComponent";
import { checkAsync, flagAsync } from "../Utils/CellFunctions";


// this component render a dummy board before initialize
const BoardComponent:FC<BoardProps> = (props: BoardProps) =>
{
   const [state, dispatch] = useReducer(gameReducer, gameInitialState);

   const performClickAction =       
      async (clickedCell:Cell, 
         action:(state:GameReducerState, cell:CellCoordinates)=>Promise<RequestResult<Board>>) => {

      let clickedCoords:CellCoordinates = { Column: clickedCell.column, Row: clickedCell.row };
      let cellCheckResult =  await action(state, clickedCoords);

      if (cellCheckResult?.error){
         props.setMessage(cellCheckResult?.error);
         props.setHasError(true);
      }
      else
      {
         dispatch({
            type: gameReducerActions.SET_BOARD,
            payload: cellCheckResult?.data
         });
      }
   }

   const onCellClick = async (clickedCell:Cell) =>{
      await performClickAction(clickedCell, checkAsync);
   }

   const onAlternativeClick = async (clickedCell:Cell) =>{
      await performClickAction(clickedCell, flagAsync);
   }

   let rows:Cell[][] = []
  
   if(state.activeBoard){
      for (let i = 1; i <= state.rows; i++) {
         let rowCells = state.activeBoard.cells.filter(c => c.row != i)
         rows.push(rowCells)
      }  
   }

   return (
         <>
         {
            rows.map(
               (row, i) => (     
                  <RowComponent  
                        key={`row-${i+1}`}
                        onCellMainClick={ onCellClick }
                        onCellAlternativeClick={ onAlternativeClick }
                        cells= { row }
                        row= { i + 1 }
                        gameStatus= {GameStatus.Active}
                  ></RowComponent>
            ))
         }
         </> 


   );

}

export default BoardComponent;
  


