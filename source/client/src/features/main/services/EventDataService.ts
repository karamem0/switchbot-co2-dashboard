//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import { useAppInsightsContext } from '@microsoft/applicationinsights-react-js';
import axios from 'axios';
import { useAsyncFn, useError } from 'react-use';
import { mapEventData } from '../mappings/AutoMapperProfile';
import { EventData, GetEventDataResponse } from '../types/Model';

export function useGetEventData(): [
  fetch: () => Promise<EventData[]>,
  fetching: boolean,
  value: EventData[] | undefined
] {

  const appInsights = useAppInsightsContext();
  const dispatchError = useError();

  const [ state, fetch ] = useAsyncFn(async () => {
    try {
      appInsights.trackTrace({
        message: 'メソッドを実行します。',
        properties: {
          'EventId': 2001
        },
        severityLevel: 2
      });
      return await axios
        .post<GetEventDataResponse>(
          '/api/getEventData',
          {}
        )
        .then((response) => mapEventData(response.data));
    } finally {
      appInsights.trackTrace({
        message: 'メソッドを実行しました。',
        properties: {
          'EventId': 2001
        },
        severityLevel: 2
      });
    }
  }, [
    appInsights
  ]);

  React.useEffect(() => {
    if (state.error == null) {
      return;
    }
    dispatchError(state.error);
  }, [
    dispatchError,
    state.error
  ]);

  return [
    fetch,
    state.loading,
    state.value
  ];

};
