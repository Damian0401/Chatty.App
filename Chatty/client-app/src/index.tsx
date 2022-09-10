import { ChakraProvider } from '@chakra-ui/react';
import ReactDOM from 'react-dom/client';
import theme from './app/common/theme';
import App from './app/layout/App';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <ChakraProvider theme={theme}>
    <App />
  </ChakraProvider>
);

