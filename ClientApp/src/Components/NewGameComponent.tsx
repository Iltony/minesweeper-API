
import { FC } from "react"
import { NewGameProps } from "../Utils/Interfaces";

const NewGameComponent:FC<NewGameProps> = (props: NewGameProps) =>
{
   return (
      <h4 className="welcome">New Game</h4>
   );
}

export default NewGameComponent;