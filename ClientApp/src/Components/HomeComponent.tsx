import React, { ChangeEvent, EventHandler, FC, MouseEventHandler, useContext, useReducer, useState } from "react"
import Switch from '@material-ui/core/Switch'
import TextField from '@material-ui/core/TextField';

import { GameConfigurationContext } from "./GameConfigurationContext"
import { HomeProps } from "../Utils/Interfaces";
import gameReducer, { gameInitialState, gameReducerConstants } from "../Utils/GameReducer";
import { getUserAsync } from "../Utils/UserFunctions"
import { Box, Button, FormControlLabel, FormGroup, Typography } from "@material-ui/core";

const HomeComponent:React.FC<HomeProps> = (props:HomeProps) => {

   
   const useAnonymous = useContext(GameConfigurationContext).allowAnomymusUser;

   const [username, setUsername] = useState("");

   const [useUsername, toggleUsername] = useState(false);


   const [state, dispatch] = useReducer(gameReducer, gameInitialState);
   const allowAnomymusUser = useContext(GameConfigurationContext).allowAnomymusUser;

   const onToggleUsernameChange = () => {
      toggleUsername(!useUsername)
   }

   const onUsernameClick = async () => {  

      let user =  await getUserAsync(username)
      
      window.location.replace("/play")
   }

   const onRegisterClick = () => {
      window.location.replace("/register")
   }


   const onUsernameChange = (event: ChangeEvent<HTMLInputElement>) => {
 
      if (event &&  event.target && event.target.value !== username){ 
         setUsername(event.target.value)
      }
   }

   let showButtonUsername = false
   
   if ( username && useUsername )
      showButtonUsername = true

   return (

         <>
         <Typography className="welcome" variant="h4">{`Welcome`}</Typography>

         <Box m="3rem" />
         <FormGroup row>

           
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
            
      

            <Box m="2rem" />
         </FormGroup>


         <FormGroup row >

           
            {useUsername &&
               <TextField id="txtusername" variant="outlined" type="text" label="username" value={username} onChange={onUsernameChange}/>
            }              

            <Box m="2rem" />
         </FormGroup>


         <FormGroup row>
                  
            {useUsername &&
               <>
                  <Button variant="outlined" color="primary" id="btnLoadUserBoards" onClick={onUsernameClick}> Load Boards</Button>
               </>
            }
            
            <Box m="2rem" />
            {useUsername &&
                  <Button  variant="outlined" color="primary"  id="btnStartNewGame" onClick={() => window.location.replace("/newgame") }> New Game </Button>
            }

         
            <Box m="2rem" />
         </FormGroup>


         <FormGroup row>


         {!username && !useUsername && useAnonymous &&
            <Button variant="outlined" color="primary"  id="btnStartAnonymous" onClick={() => {window.location.replace("/newgame")}}> New Game A </Button>
         }
         </FormGroup>
         </>

   );
}


export default HomeComponent;