import { Button, TextField, Typography } from "@material-ui/core";
import { format } from "date-fns";
import { ChangeEvent, FC, useReducer, useState, useContext } from "react"
import gameReducer, { gameInitialState, gameReducerActions } from "../Utils/GameReducer";
import { RegisterProps, User } from "../Utils/Interfaces";
import { registerUserAsync } from "../Utils/UserFunctions";
import useStyles from "../Utils/UseStyles";
import { GameConfigurationContext } from "./GameConfigurationContext";


const RegisterComponent:FC<RegisterProps> = (props: RegisterProps) =>
{
   const gameDefaults = useContext(GameConfigurationContext); 

   const initialUserData:User = {
      username: '',
      birthdate: gameDefaults.maxAllowedBirthDate
   };

   const classes = useStyles();
   const [userData, setUserData] = useState<User>(initialUserData)
   const [state, dispatch] = useReducer(gameReducer, gameInitialState);
   const [message, setMessage] = useState("");
   const [hasError, setHasError] = useState(false);
   const [showNewGame, toggleNewGame] = useState(false);
   

   const onRegisterClick = async () => {  
     
      if (!userData.username){
         setMessage("Must specify a username")
         setHasError(true);
         return;
      }
      
      setMessage("");
      setHasError(false)
      
      let userRegistratioResult =  await registerUserAsync(userData, gameDefaults.minimumAllowedAge)

      if (userRegistratioResult?.error){
         setMessage(userRegistratioResult?.error)
         setHasError(true)
      }
      else
      {
            dispatch({
               type: gameReducerActions.SET_USER,
               payload: userRegistratioResult?.data
            });

            setMessage("Registration complete")
            toggleNewGame(true);
      }
   }




   const onUsernameChange = (event: ChangeEvent<HTMLInputElement>) =>{
      setUserData({...userData, username: event.target.value})
   }

   const onFirstnameChange = (event: ChangeEvent<HTMLInputElement>) =>{
      setUserData({...userData, firstName: event.target.value})
   }
   
   const onLastnameChange = (event: ChangeEvent<HTMLInputElement>) =>{
      setUserData({...userData, lastName: event.target.value})
   }
   
   const onBirthdateChange = (event: ChangeEvent<HTMLInputElement>) =>{
      setUserData({...userData, birthdate: new Date(event.target.value)})
   }

      return (
            <form className={classes.vertical}  noValidate>
               <Typography className="welcome" variant="h4">{`Register`}</Typography>

               <TextField className={classes.textField} id="txtUsername" variant="outlined" type="text" label="Username" value={userData.username} onChange={onUsernameChange}/>
               <TextField className={classes.textField} id="txtFirstName" variant="outlined" type="text" label="First Name" value={userData.firstName} onChange={onFirstnameChange}/>
               <TextField className={classes.textField} id="txtLastName" variant="outlined" type="text" label="Last Name" value={userData.lastName} onChange={onLastnameChange}/>

               <TextField
                  id="dtpBirthdate"
                  label="Birthdate"
                  variant="outlined"
                  type="date"

                  defaultValue={format(userData.birthdate as Date, "yyyy-MM-dd")}
                  className={classes.textField}
                  onChange={onBirthdateChange}
                  InputLabelProps={{ shrink: true }}
               />


               {!showNewGame && 
                  <Button className={classes.button} variant="contained" color="primary"  id="btnRegister" onClick={onRegisterClick}>Register</Button>
               }

               {message &&
                  <Typography className={hasError ? 'error' : 'welcome'} variant="h6">{message}</Typography>
               }

               {showNewGame &&
                    <Button className={classes.button} variant="contained" color="primary"  id="btnNewGame" onClick={() => window.location.replace("/newGame")}>Start</Button>
               }

               <Button className={classes.button} variant="outlined" color="secondary"  id="btnGoHome" onClick={() => window.location.replace("/")}> Go home </Button>
          </form>
      );
}

export default RegisterComponent;