//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

import {
  createMap,
  createMapper,
  forMember,
  mapFrom
} from '@automapper/core';
import { PojosMetadataMap, pojos } from '@automapper/pojos';
import { EventData, GetEventDataResponse, GetEventDataResponseValue } from '../types/Model';

const mapper = createMapper({
  strategyInitializer: pojos()
});

PojosMetadataMap.create<GetEventDataResponseValue>('GetEventDataResponseValue', {
  battery: String,
  CO2: String,
  deviceMac: String,
  deviceName: String,
  humidity: String,
  lastUpdateTime: String,
  scale: String,
  temperature: String
});

PojosMetadataMap.create<EventData>('EventData', {
  battery: String,
  CO2: String,
  deviceMac: String,
  deviceName: String,
  humidity: String,
  lastUpdateTime: Date,
  scale: String,
  temperature: String
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
