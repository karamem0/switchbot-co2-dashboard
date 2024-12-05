//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './MainPage.presenter';

import { InteractionStatus } from '@azure/msal-browser';
import { useMsal } from '@azure/msal-react';
import axios from 'axios';
import { useInterval } from 'react-use';
import { loginParams } from '../../../config/MsalConfig';
import { useGetEventData } from '../services/EventDataService';

function MainPage() {

  const msal = useMsal();

  const [ initialized, setInitialized ] = React.useState<boolean>(false);

  const [
    loading,
    value,
    fetch
  ] = useGetEventData();

  React.useEffect(() => {
    (async () => {
      if (msal.inProgress !== InteractionStatus.None) {
        return;
      }
      if (msal.accounts.length > 0) {
        msal.instance.setActiveAccount(msal.accounts[0]);
        axios.interceptors.request.use(async (request) => {
          const authResult = await msal.instance.acquireTokenSilent(loginParams);
          const accessToken = authResult.accessToken;
          request.headers.Authorization = `Bearer ${accessToken}`;
          return request;
        });
        setInitialized(true);
        await fetch();
      } else {
        msal.instance.loginRedirect(loginParams);
      }
    })();
  }, [
    msal,
    fetch
  ]);

  useInterval(async () => {
    await fetch();
  }, 60000);

  return (
    <Presenter
      items={value}
      loading={!initialized || loading} />
  );

};

export default MainPage;
