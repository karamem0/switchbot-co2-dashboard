//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import axios from 'axios';
import { useAsyncFn, useError } from 'react-use';
import { mapEventData } from '../mappings/AutoMapperProfile';
import { EventData, GetEventDataResponse } from '../types/Model';

async function getEventData(): Promise<EventData[]> {
  return await axios
    .post<GetEventDataResponse>(
      '/api/getEventData',
      {}
    )
    .then((response) => mapEventData(response.data));
}

export function useGetEventData(): [
  loading: boolean,
  value: EventData[] | undefined,
  fetch: typeof getEventData
] {

  const dispatchError = useError();
  const [ state, fetch ] = useAsyncFn(getEventData);

  React.useEffect(() => {
    if (!state.error) {
      return;
    }
    dispatchError(state.error);
  }, [
    dispatchError,
    state
  ]);

  return [
    state.loading,
    state.value,
    fetch
  ];

};
