//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public record ExecuteWebhookRequest
{

    [JsonPropertyName("eventType")]
    public required string EventType { get; set; }

    [JsonPropertyName("eventVersion")]
    public required string EventVersion { get; set; }

    [JsonPropertyName("context")]
    public required ExecuteWebhookRequestContext Context { get; set; }

}

public record ExecuteWebhookRequestContext
{

    [JsonPropertyName("deviceType")]
    public required string DeviceType { get; set; }

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

    [JsonPropertyName("timeOfSample")]
    public required long TimeOfSample { get; set; }

}
