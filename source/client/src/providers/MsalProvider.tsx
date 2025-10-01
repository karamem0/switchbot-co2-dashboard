//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import { MsalProvider as Provider } from '@azure/msal-react';
import { PublicClientApplication } from '@azure/msal-browser';
import { msalConfig } from '../config/MsalConfig';

function MsalProvider(props: Readonly<React.PropsWithChildren<unknown>>) {

  const { children } = props;

  return (
    <Provider instance={new PublicClientApplication(msalConfig)}>
      {children}
    </Provider>
  );

}

export default MsalProvider;
