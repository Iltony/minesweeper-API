import { Typography } from "@material-ui/core";
import { FC } from "react"
import { CellProps } from "../Utils/Interfaces";
import useStyles from "../Utils/UseStyles";
import { CellStatus, GameStatus } from "../Utils/CellConstants";
import mineLogo from '../Assets/mine-logo.png';
import flagLogo from '../Assets/flag-logo.png';
import mineSaved from '../Assets/mine-saved.png';
import questionLogo from '../Assets/question-logo.png';
import '../Assets/App.css';


const CellComponent:FC<CellProps> = (props: CellProps) =>
{


   const classes = useStyles();
   
   const onCellClick = (event: any) =>{
      if (event.type === 'click') {
        props.onCellMainClick(props.cell)

      } else if (event.type === 'contextmenu') {
        props.onCellAlternativeClick(props.cell)
      }
   }

    if (props.gameStatus == GameStatus.GameOver){

        if (props.cell.itIsAMine && props.cell.status == CellStatus.Flagged) {
        return (
                  
            
            <div id={`cell-${props.cell.position}`} onClick={onCellClick} className="cell cell-saved" contextMenu="none">
                <img src={mineSaved} alt="savedLogo" />
            </div>);
        }
        else if (props.cell.itIsAMine && props.cell.status != CellStatus.Flagged) {
            return (<div className="cell cell-burned" contextMenu="none">
                <img src={mineLogo} alt="mineLogo" />
            </div>)
        }
    }
    else {
       
        if(props.cell.status = CellStatus.Revealed){
             
            if (props.cell.closerMinesNumber != null && props.cell.closerMinesNumber > 0){
                return (<div className="cell cell-revealed" contextMenu="none">
                    <Typography className="cell-font" variant="h1">{props.cell.closerMinesNumber}</Typography>
                </div>)
            }
            else {
                return(<div className="cell cell-revealed" contextMenu="none"></div>);
            }

        } else if(props.cell.status = CellStatus.Flagged){
            return (<div className="cell cell-flagged" contextMenu="none">
                <img src={flagLogo} alt="flaggedLogo" />
            </div>);

        } else if(props.cell.status = CellStatus.Suspicious){
            return (<div className="cell cell-suspicious" contextMenu="none">
                <img src={questionLogo} alt="questionLogo" />
            </div>);
        }

    }


    return(<div className="cell" contextMenu="none"></div>);

}

export default CellComponent;
  

