import { ChakraProvider } from '@chakra-ui/react';
import ReactDOM from 'react-dom/client';
import theme from './app/common/utils/theme';
import App from './app/layout/App';
import { store, StoreContext } from './app/stores/store'
import { unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import 'react-toastify/dist/ReactToastify.min.css';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

export const history = createBrowserHistory();

root.render(
  <StoreContext.Provider value={store}>
    <ChakraProvider theme={theme}>
      <HistoryRouter history={history}>
        <App />
      </HistoryRouter>
    </ChakraProvider>
  </StoreContext.Provider>
);

