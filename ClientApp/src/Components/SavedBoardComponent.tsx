import { FC } from "react"
import { SavedBoardsProps } from "../Utils/Interfaces";


const SavedBoardComponent:FC<SavedBoardsProps> = (props: SavedBoardsProps) =>
{
   return (
         <h4 className="welcome">{`Present a list of boards`}</h4>
   );
}



export default SavedBoardComponent;