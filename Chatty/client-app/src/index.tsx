import { ChakraProvider } from '@chakra-ui/react';
import ReactDOM from 'react-dom/client';
import theme from './app/common/theme';
import App from './app/layout/App';
import { unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import 'react-toastify/dist/ReactToastify.min.css';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

export const history = createBrowserHistory();

root.render(
  <ChakraProvider theme={theme}>
    <HistoryRouter history={history}>
      <App />
    </HistoryRouter>
  </ChakraProvider>
);

