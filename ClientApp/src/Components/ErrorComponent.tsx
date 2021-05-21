import { FC } from "react"
import { ErrorProps } from "../Utils/Interfaces";

const ErrorComponent:FC<ErrorProps> = (props: ErrorProps) =>
{

   return (
      <h4 className="Error">Something happens</h4>
   );
}

export default ErrorComponent;