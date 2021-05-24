import { FC } from "react"
import { Cell, RowProps } from "../Utils/Interfaces";

import CellComponent from './CellComponent';
import '../Assets/App.css';


const RowComponent:FC<RowProps> = (props: RowProps) =>
{
    return (
        
        <div className="row" key={`row-${props.row}`}>
            {
                props.cells.map((cell:Cell) => (
                    <CellComponent 
                        key={`cell-${cell.column}-${cell.row}`}
                        gameStatus={props.gameStatus}
                        cell={cell}
                        onCellMainClick={props.onCellMainClick}
                        onCellAlternativeClick={props.onCellAlternativeClick}
                    ></CellComponent>
            ))}
        </div>
    );
}

export default RowComponent;
  

