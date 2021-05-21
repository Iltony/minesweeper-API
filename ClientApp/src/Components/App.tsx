//import logo from '../Assets/logo.svg';
import logo from '../Assets/logo.png';
import '../Assets/App.css';

import { BrowserRouter } from 'react-router-dom';
import { GameConfigurationProvider } from './GameConfigurationContext'
import routes from '../Utils/Routes'
function App() {
	return (

		<div className="App">
			<header className="App-header">

				<img src={logo} className="App-logo" alt="logo" />
				{/* <p className="hometitle"> Minesweeper </p> */}


				<GameConfigurationProvider>

					<BrowserRouter children={routes} basename={"/"} />

				</GameConfigurationProvider>

			</header>

		</div>

	);

}

export default App;
