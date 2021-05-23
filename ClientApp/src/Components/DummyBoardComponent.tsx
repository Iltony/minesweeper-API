import { FC, useReducer, useState } from "react"
import gameReducer, { gameInitialState, gameReducerActions } from "../Utils/GameReducer";
import { BoardProps, Cell, CellCoordinates } from "../Utils/Interfaces";
import { CellStatus, GameStatus } from "../Utils/CellConstants";
import { initializeAsync } from "../Utils/BoardFunctions";
import RowComponent from "./RowComponent";


// this component render a dummy board before initialize
const DummyBoardComponent:FC<BoardProps> = (props: BoardProps) =>
{

   const [state, dispatch] = useReducer(gameReducer, gameInitialState);
   
   const onCellClick = async (clickedCell:Cell) => {

      let clickedCoords:CellCoordinates = { Column: clickedCell.column, Row: clickedCell.row };

      let boardInitializarionResult =  await initializeAsync(state, clickedCoords);
      
      if (boardInitializarionResult?.error){
         props.setMessage(boardInitializarionResult?.error);
         props.setHasError(true);
      }
      else
      {
            dispatch({
               type: gameReducerActions.SET_BOARD,
               payload: boardInitializarionResult?.data
            });
      }
   }

   const onAlternativeCellClicked = async (clickedCell:Cell) => {
      props.setMessage('To start clic with the main button');
      props.setHasError(true);
   }



   const getDummyCell = (column:number, row:number, position:number) => (
         {
      column: column,
      row: row,
      position: position,
      status: CellStatus.Clear,
      itIsAMine: false,
      closerMinesNumber: 0
   } as unknown as Cell);

   const getDummyCells = () => {
      let cellCounter:number = 0
      let cells:Cell[]= []

      for (let i = 1; i <= state.columns; i++){
         for (let j = 1; j <= state.rows; j++){
            
            cells.push(getDummyCell(i,j,cellCounter++));

         }  
      }

      return cells;
   }

   let rows:Cell[][] = []
   let allTheCells = getDummyCells()

   
   for (let i = 1; i <= state.rows; i++) {
      let rowCells = allTheCells.filter(c => c.row == i)

      rows.push(rowCells)
   }  


   return (
         <>
            {
               rows.map(
                  (row, i) => (     
                     <RowComponent  
                           key={`row-${i+1}`}
                           onCellMainClick={ onCellClick }
                           onCellAlternativeClick={ onAlternativeCellClicked }
                           cells= { row }
                           row= { i + 1 }
                           gameStatus= {GameStatus.Active}
                     ></RowComponent>
               ))
            }
        </> 
   );

}

export default DummyBoardComponent;
  
