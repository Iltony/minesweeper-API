import {Route, Switch} from 'react-router-dom'
import ErrorComponent from '../Components/ErrorComponent';
import HomeComponent from '../Components/HomeComponent';

import NewGameComponent from '../Components/NewGameComponent';
import GameComponent from '../Components/GameComponent';
import RegisterComponent from '../Components/RegisterComponent';
import SavedBoardComponent from '../Components/SavedBoardComponent';


const Routes = (
    <Switch>
        <Route path="/error" component={ErrorComponent} />

        <Route path="/savedBoards" component={SavedBoardComponent} />

        <Route path="/newGame" component={NewGameComponent} />v

        <Route path="/play" component={GameComponent} />
        
        <Route path="/register" component={RegisterComponent} />

        <Route path="/" component={HomeComponent} />

    </Switch>
)

export default Routes;