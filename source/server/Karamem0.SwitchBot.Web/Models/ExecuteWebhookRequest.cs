//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public class ExecuteWebhookRequest
{

    [JsonPropertyName("eventType")]
    public required string EventType { get; set; }

    [JsonPropertyName("eventVersion")]
    public required string EventVersion { get; set; }

    [JsonPropertyName("context")]
    public required ExecuteWebhookRequestContext Context { get; set; }

}

public class ExecuteWebhookRequestContext
{

    [JsonPropertyName("deviceType")]
    public required string DeviceType { get; set; }

    [JsonPropertyName("deviceMac")]
    public required string DeviceMac { get; set; }

    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }

    [JsonPropertyName("scale")]
    public required string Scale { get; set; }

    [JsonPropertyName("humidity")]
    public float Humidity { get; set; }

    [JsonPropertyName("CO2")]
    public required int CO2 { get; set; }

    [JsonPropertyName("battery")]
    public int Battery { get; set; }

    [JsonPropertyName("timeOfSample")]
    public long TimeOfSample { get; set; }

}
