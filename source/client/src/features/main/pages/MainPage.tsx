//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './MainPage.presenter';

import { InteractionStatus } from '@azure/msal-browser';
import { loginParams } from '../../../config/MsalConfig';
import { useMsal } from '@azure/msal-react';

function MainPage() {

  const msal = useMsal();

  React.useEffect(() => {
    (async () => {
      if (msal.inProgress === InteractionStatus.None) {
        if (msal.accounts.length > 0) {
          msal.instance.setActiveAccount(msal.accounts[0]);
        } else {
          msal.instance.loginRedirect(loginParams);
        }
      }
    })();
  }, [
    msal
  ]);

  return (
    <Presenter />
  );

};

export default MainPage;
