//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import Presenter from './Error500Page.presenter';

import { useAppInsightsContext } from '@microsoft/applicationinsights-react-js';

interface Error500PageProps {
  error?: Error
}

function Error500Page(props: Readonly<Error500PageProps>) {

  const { error } = props;

  const appInsights = useAppInsightsContext();

  React.useCallback(() => {
    if (error == null) {
      return;
    }
    appInsights.trackException({ error });
  }, [
    appInsights,
    error
  ]);

  return (
    <Presenter />
  );

}

export default Error500Page;
