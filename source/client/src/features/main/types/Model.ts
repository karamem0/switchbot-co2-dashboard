//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

export interface GetEventDataResponse {
  value: GetEventDataResponseValue[]
}

export interface GetEventDataResponseValue {
  battery?: number,
  CO2?: number,
  deviceMac?: string,
  deviceName?: string,
  humidity?: number,
  lastUpdateTime?: string,
  scale?: string,
  temperature?: number
}

export interface EventData {
  battery?: number,
  CO2?: number,
  deviceMac?: string,
  deviceName?: string,
  humidity?: number,
  lastUpdateTime?: Date,
  scale?: string,
  temperature?: number
}
