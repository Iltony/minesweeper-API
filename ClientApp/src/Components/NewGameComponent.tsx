import { Button, TextField, Typography } from "@material-ui/core";
import { ChangeEvent, FC, useState } from "react"
import { useHistory } from "react-router-dom";
import { useGameDispatch, useGameState } from "../Utils/GameContext";
import { gameReducerActions } from "../Utils/GameReducer";
import { NewGameProps } from "../Utils/Interfaces";
import useStyles from "../Utils/UseStyles";

const NewGameComponent:FC<NewGameProps> = (props: NewGameProps) =>
{
   const classes = useStyles();
   
   const gameState = useGameState();
   const dispatch = useGameDispatch();
   const history = useHistory();

   const [message, setMessage] = useState("");
   const [hasError, setHasError] = useState(false);
   
   const initialTmpState = { columns:gameState.columns, rows:gameState.rows, mines:gameState.mines };

   const [tmpState, setTempState] = useState(initialTmpState);
      
   const validate = () => {

      if (isNaN(tmpState.columns as number)) {
         setMessage("Columns must be a number")
         setHasError(true)
         return false;
      }

      if (isNaN(tmpState.rows as number)) {
         setMessage("Rows must be a number")
         setHasError(true)
         return false;
      }

      if (isNaN(tmpState.mines as number)) {
         setMessage("Mines must be a number")
         setHasError(true)
         return false;
      }

      return true;
   }


   const onColumnsChange = (event: ChangeEvent<HTMLInputElement>) =>{

      if (!/^\d*$/.test(event.target.value)) {
         setMessage("Columns must be a number")
         setHasError(true)
         return;
      }
      else {
         const value:number = parseInt(event.target.value);
         setTempState({...tmpState, columns:value});
      }

   }

   const onRowsChange = (event: ChangeEvent<HTMLInputElement>) =>{
      if (!/^\d*$/.test(event.target.value)) {
         setMessage("Rows must be a number")
         setHasError(true)
         return;
      }
      else {
         const value:number = parseInt(event.target.value);
         setTempState({...tmpState, rows:value});
      }
   }
   
   const onMinesChange = (event: ChangeEvent<HTMLInputElement>) =>{
      if (!/^\d*$/.test(event.target.value)) {
         setMessage("Mines must be a number")
         setHasError(true)
         return;
      }
      else {
         const value:number = parseInt(event.target.value);
         setTempState({...tmpState, mines:value});
      }
   }


   const onStartClick = async () => {  
      if (validate()){
         dispatch({
            type: gameReducerActions.SET_GAME_SETTINGS, 
            payload: tmpState
         });

         history.push('/play')
      }
   }



  
   return (
      <form className={classes.vertical}  noValidate>
         <Typography className="welcome" variant="h4">{`New Game`}</Typography>

         <TextField className={classes.textField} id="txtColumns" variant="outlined" type="text" label="Columns" value={tmpState.columns} onChange={onColumnsChange}/>
         <TextField className={classes.textField} id="txtRows" variant="outlined" type="text" label="Rows" value={tmpState.rows} onChange={onRowsChange}/>
         <TextField className={classes.textField} id="txtMines" variant="outlined" type="text" label="Mines" value={tmpState.mines} onChange={onMinesChange}/>


         {message &&
            <Typography className={hasError ? 'error' : 'Welcome'} variant="body2">{message}</Typography>
         }

         <Button className={classes.button} variant="outlined" color="primary"  id="btnStart" onClick={onStartClick}>Start</Button>
         <Button className={classes.button} variant="outlined" color="secondary"  id="btnGoHome" onClick={() => history.push("/")}> Go home </Button>
      </form>
   );


}

export default NewGameComponent;