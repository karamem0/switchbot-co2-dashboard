//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';
import ReactDOM from 'react-dom/client';

import * as ress from 'ress';
import {
  BrowserRouter,
  Route,
  Routes
} from 'react-router-dom';
import Error404Page from './features/error/pages/Error404Page';
import Error500Page from './features/error/pages/Error500Page';
import { ErrorBoundary } from 'react-error-boundary';
import { Global } from '@emotion/react';
import IntlProvider from './providers/IntlProvider';
import MainPage from './features/main/pages/MainPage';
import MsalProvider from './providers/MsalProvider';
import ThemeProvider from './providers/ThemeProvider';

ReactDOM
  .createRoot(document.getElementById('root') as Element)
  .render(
    <React.StrictMode>
      <Global styles={ress} />
      <ThemeProvider>
        <IntlProvider>
          <BrowserRouter>
            <ErrorBoundary
              fallbackRender={(props) => (
                <Error500Page {...props} />
              )}>
              <Routes>
                <Route
                  path="/"
                  element={(
                    <MsalProvider>
                      <MainPage />
                    </MsalProvider>
                  )} />
                <Route
                  path="*"
                  element={(
                    <Error404Page />
                  )} />
              </Routes>
            </ErrorBoundary>
          </BrowserRouter>
        </IntlProvider>
      </ThemeProvider>
    </React.StrictMode>
  );
