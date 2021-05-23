import '../Assets/App.css';
import logo from '../Assets/logo.png';

import { BrowserRouter } from 'react-router-dom';
import { GameConfigurationProvider } from './GameConfigurationContext'
import routes from '../Utils/Routes'

function App() {
	return (

		<div className="App">
			<header className="App-header">
				<img src={logo} className="App-logo" alt="logo" />
				<GameConfigurationProvider>

					<BrowserRouter children={routes} basename={"/"} />

				</GameConfigurationProvider>

			</header>
		</div>
	);
}

export default App;
