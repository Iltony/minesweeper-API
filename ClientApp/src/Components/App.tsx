import '../Assets/App.css';
import logo from '../Assets/logo.png';

import { BrowserRouter } from 'react-router-dom';
import { GameConfigurationProvider } from '../Utils/GameConfigurationContext'
import routes from '../Utils/Routes';
import { GameProvider } from '../Utils/GameContext';

function App() {

	const isHome = window.location.pathname === '/';

	return (

		<div className="App">
			<header className="App-header">

				{isHome &&
					<img src={logo} className="App-logo" alt="logo" />
				}

				<GameConfigurationProvider>
					<GameProvider>
						<BrowserRouter children={routes} basename={"/"} />
					</GameProvider>
				</GameConfigurationProvider>

			</header>
		</div>
	);
}

export default App;
