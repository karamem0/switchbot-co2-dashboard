//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public class GetEventDataResponse
{

    [JsonPropertyName("value")]
    public required GetEventDataResponseValue[] Value { get; set; }

}

public class GetEventDataResponseValue
{

    [JsonPropertyName("deviceName")]
    public required string DeviceName { get; set; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; set; }

    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }

    [JsonPropertyName("scale")]
    public required string Scale { get; set; }

    [JsonPropertyName("humidity")]
    public float Humidity { get; set; }

    [JsonPropertyName("CO2")]
    public int CO2 { get; set; }

    [JsonPropertyName("battery")]
    public int Battery { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public DateTimeOffset LastUpdateTime { get; set; }

}
