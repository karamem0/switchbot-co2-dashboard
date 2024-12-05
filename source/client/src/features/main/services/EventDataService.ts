//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import React from 'react';

import { EventData, GetEventDataResponse } from '../types/Model';
import { useAsyncFn, useError } from 'react-use';
import axios from 'axios';
import { mapEventData } from '../mappings/AutoMapperProfile';

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
