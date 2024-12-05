//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import { EventData, GetEventDataResponse, GetEventDataResponseValue } from '../types/Model';
import { PojosMetadataMap, pojos } from '@automapper/pojos';
import {
  createMap,
  createMapper,
  forMember,
  mapFrom
} from '@automapper/core';

const mapper = createMapper({
  strategyInitializer: pojos()
});

PojosMetadataMap.create<GetEventDataResponseValue>('GetEventDataResponseValue', {
  deviceName: String,
  deviceMac: String,
  temperature: String,
  scale: String,
  humidity: String,
  CO2: String,
  battery: String,
  lastUpdateTime: String
});

PojosMetadataMap.create<EventData>('EventData', {
  deviceName: String,
  deviceMac: String,
  temperature: String,
  scale: String,
  humidity: String,
  CO2: String,
  battery: String,
  lastUpdateTime: Date
});

createMap<GetEventDataResponseValue, EventData>(
  mapper,
  'GetEventDataResponseValue',
  'EventData',
  forMember((target) => target.lastUpdateTime, mapFrom((source) => source.lastUpdateTime ? new Date(source.lastUpdateTime) : undefined))
);

export function mapEventData(value: GetEventDataResponse): EventData[] {
  return mapper.mapArray<GetEventDataResponseValue, EventData>(
    value.value,
    'GetEventDataResponseValue',
    'EventData'
  );
}
