//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';
import ReactDOM from 'react-dom/client';

import { Global } from '@emotion/react';
import { ErrorBoundary } from 'react-error-boundary';
import {
  BrowserRouter,
  Route,
  Routes
} from 'react-router-dom';
import * as ress from 'ress';
import Error404Page from './features/error/pages/Error404Page';
import Error500Page from './features/error/pages/Error500Page';
import MainPage from './features/main/pages/MainPage';
import IntlProvider from './providers/IntlProvider';
import MsalProvider from './providers/MsalProvider';
import TelemetryProvider from './providers/TelemetryProvider';
import ThemeProvider from './providers/ThemeProvider';

ReactDOM
  .createRoot(document.getElementById('root') as Element)
  .render(
    <React.StrictMode>
      <Global styles={ress} />
      <BrowserRouter>
        <TelemetryProvider>
          <IntlProvider>
            <ThemeProvider>
              <ErrorBoundary
                fallbackRender={(props) => (
                  <Error500Page error={props.error as Error} />
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
            </ThemeProvider>
          </IntlProvider>
        </TelemetryProvider>
      </BrowserRouter>
    </React.StrictMode>
  );
