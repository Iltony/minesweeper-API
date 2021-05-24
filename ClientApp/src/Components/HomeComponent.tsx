import React, { ChangeEvent, useContext, useState } from "react"
import Switch from '@material-ui/core/Switch'
import TextField from '@material-ui/core/TextField';

import { GameConfigurationContext } from "../Utils/GameConfigurationContext"
import { HomeProps } from "../Utils/Interfaces";
import { gameReducerActions } from "../Utils/GameReducer";
import { getUserAsync } from "../Utils/UserFunctions"
import { Button, FormControlLabel } from "@material-ui/core";
import useStyles from "../Utils/UseStyles";
import { useGameDispatch } from "../Utils/GameContext";
import { useHistory } from "react-router-dom";

const HomeComponent:React.FC<HomeProps> = (props:HomeProps) => {

   const classes = useStyles();
   
   const [username, setUsername] = useState("");
   const [useUsername, toggleUsername] = useState(false);
   const [errorMessage, setErrorMessage] = useState("");

   const allowAnomymusUser = useContext(GameConfigurationContext).allowAnomymusUser;
   const dispatch = useGameDispatch();
   const history = useHistory();
   
   const onToggleUsernameChange = () => {
      toggleUsername(!useUsername)
   }

   const onUsernameClick = async () => {  

      let user =  await getUserAsync(username)

      if (user){
         dispatch({
            type: gameReducerActions.SET_USER,
            payload: user
         });
         
         history.push("/play")
      }
      else {
         setErrorMessage("No se encuentra el usuario")
      }

      console.log(user, 2, undefined);
      
   }

   const onUsernameChange = (event: ChangeEvent<HTMLInputElement>) => {
 
      if (event &&  event.target && event.target.value !== username){ 
         setUsername(event.target.value)
      }
   }

   const usernameError = () => errorMessage !== ""

   return (
      <form className={classes.vertical} noValidate>

         <FormControlLabel
            control={
               <Switch
               checked={useUsername}
               onChange={onToggleUsernameChange}
               name="swcUseUsername"
               inputProps={{ 'aria-label': 'secondary checkbox' }}
               />
            }
            label="Load my saved games "
            />
         

            {useUsername &&
               <TextField id="txtusername" variant="outlined" type="text" label="username" 
                     className={classes.textField}
                     error={usernameError()} 
                     helperText={`${errorMessage}`}
                     value={username} onChange={onUsernameChange}/>
            }              

            {useUsername &&
                 <Button className={classes.button} variant="outlined" color="primary" id="btnLoadUserBoards" onClick={onUsernameClick}> Load Boards</Button>
            }
            
            {useUsername &&
                  <Button className={classes.button} variant="contained" color="primary"  id="btnStartNewGame" onClick={() => history.push("/newgame") }> New Game </Button>
            }

         {!username && !useUsername && allowAnomymusUser &&
            <Button className={classes.button} variant="contained" color="primary"  id="btnStartAnonymous" onClick={() => {history.push("/newgame")}}> New Game </Button>
         }

         <Button className={classes.button} variant="outlined" color="secondary"  id="btnRegister" onClick={() => {history.push("/register")}}> User Registration </Button>

      </form>
   );
}


export default HomeComponent;