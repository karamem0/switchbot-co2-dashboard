//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './MsalInteractionGate.presenter';

import { InteractionStatus } from '@azure/msal-browser';
import { useMsal } from '@azure/msal-react';

function MsalInteractionGate(props: Readonly<React.PropsWithChildren<unknown>>) {

  const { children } = props;

  const msal = useMsal();

  return (
    <Presenter loading={msal.inProgress !== InteractionStatus.None}>
      {children}
    </Presenter>
  );

}

export default MsalInteractionGate;
