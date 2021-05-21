
import { FC } from "react"
import { GameProps } from "../Utils/Interfaces";

const GameComponent:FC<GameProps> = (props: GameProps) =>
{
   return (
      <h4 className="welcome">Game placeholder</h4>
   );
}

export default GameComponent;