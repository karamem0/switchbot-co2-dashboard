//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

export interface GetEventDataResponse {
  value: GetEventDataResponseValue[]
}

export interface GetEventDataResponseValue {
  deviceName?: string,
  deviceMac?: string,
  temperature?: number,
  scale?: string,
  humidity?: number,
  CO2?: number,
  battery?: number,
  lastUpdateTime?: string
}

export interface EventData {
  deviceName?: string,
  deviceMac?: string,
  temperature?: number,
  scale?: string,
  humidity?: number,
  CO2?: number,
  battery?: number,
  lastUpdateTime?: Date
}
