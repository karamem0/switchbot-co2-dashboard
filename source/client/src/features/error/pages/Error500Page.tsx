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

  React.useEffect(() => {
    if (error == null) {
      return;
    }
    appInsights.trackException({ exception: error });
    appInsights.trackTrace({
      message: error.message,
      severityLevel: 5
    });
  }, [
    appInsights,
    error
  ]);

  return (
    <Presenter />
  );

}

export default Error500Page;
