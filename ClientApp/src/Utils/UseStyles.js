import { makeStyles } from '@material-ui/core'

const useStyles = makeStyles((theme) => ({
    container: {
      display: 'flex',
      flexWrap: 'wrap',
    },

    vertical: {
      display: 'flex',
      flexWrap: 'wrap',
      flexDirection: 'column',
      textAlign: 'center',
       
    
    },

    textField: {
      marginTop: theme.spacing(2),
      width: 200,
    },

    button: {
      marginTop: theme.spacing(2),
      width: 200,
    },
  }));

  export default useStyles;