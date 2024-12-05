//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './AuthCallbackPage.presenter';

import { broadcastResponseToMainFrame } from '@azure/msal-browser/redirect-bridge';
import { useError } from 'react-use';

function AuthCallbackPage() {

  const dispatchError = useError();

  React.useEffect(() => {
    (async () => {
      try {
        await broadcastResponseToMainFrame();
      } catch (error) {
        if (error instanceof Error) {
          dispatchError(error);
        } else {
          console.error(error);
        }
      }
    })();
  }, [
    dispatchError
  ]);

  return (
    <Presenter />
  );

}

export default AuthCallbackPage;
