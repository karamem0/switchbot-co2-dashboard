//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public record GetEventDataResponse
{

    [JsonPropertyName("value")]
    public required GetEventDataResponseValue[] Value { get; set; }

}

public record GetEventDataResponseValue
{

    [JsonPropertyName("deviceName")]
    public required string DeviceName { get; set; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; set; }

    [JsonPropertyName("temperature")]
    public required float Temperature { get; set; }

    [JsonPropertyName("scale")]
    public required string Scale { get; set; }

    [JsonPropertyName("humidity")]
    public required float Humidity { get; set; }

    [JsonPropertyName("CO2")]
    public required int CO2 { get; set; }

    [JsonPropertyName("battery")]
    public required int Battery { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public required DateTimeOffset LastUpdateTime { get; set; }

}
