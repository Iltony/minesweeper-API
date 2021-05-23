import { Button, TextField, Typography } from "@material-ui/core";
import { ChangeEvent, FC, useReducer, useState } from "react"
import gameReducer, { gameInitialState, gameReducerActions } from "../Utils/GameReducer";
import { GameReducerState, NewGameProps } from "../Utils/Interfaces";
import useStyles from "../Utils/UseStyles";

const NewGameComponent:FC<NewGameProps> = (props: NewGameProps) =>
{
   const classes = useStyles();
   
   const [state, dispatch] = useReducer(gameReducer, gameInitialState);
   const [message, setMessage] = useState("");
   const [hasError, setHasError] = useState(false);
   const stateGame:GameReducerState = state as GameReducerState;


   const onColumnsChange = (event: ChangeEvent<HTMLInputElement>) =>{
      dispatch({
         type: gameReducerActions.SET_COLUMNS,
         payload: event.target.value
      });
   }

   const onRowsChange = (event: ChangeEvent<HTMLInputElement>) =>{
      dispatch({
         type: gameReducerActions.SET_ROWS,
         payload: event.target.value
      });
   }
   
   const onMinesChange = (event: ChangeEvent<HTMLInputElement>) =>{
      dispatch({
         type: gameReducerActions.SET_MINES,
         payload: event.target.value
      });
   }
  
   return (
      <form className={classes.vertical}  noValidate>
         <Typography className="welcome" variant="h4">{`New Game`}</Typography>

         <TextField className={classes.textField} id="txtColumns" variant="outlined" type="text" label="Columns" value={stateGame.columns} onChange={onColumnsChange}/>
         <TextField className={classes.textField} id="txtRows" variant="outlined" type="text" label="Rows" value={stateGame.rows} onChange={onRowsChange}/>
         <TextField className={classes.textField} id="txtMines" variant="outlined" type="text" label="Mines" value={stateGame.mines} onChange={onMinesChange}/>

         {!hasError && 
            <Button className={classes.button} variant="outlined" color="primary"  id="btnStart" onClick={() => window.location.replace("/play")}>Start</Button>
         }

         {message &&
            <Typography className={hasError ? 'error' : 'welcome'} variant="h4">{message}</Typography>
         }

         <Button className={classes.button} variant="outlined" color="secondary"  id="btnGoHome" onClick={() => window.location.replace("/")}> Go home </Button>
      </form>
   );


}

export default NewGameComponent;