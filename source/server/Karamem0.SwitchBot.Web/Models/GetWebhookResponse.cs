//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public record GetWebhookResponse
{

    [JsonPropertyName("value")]
    public required GetWebhookResponseValue[] Value { get; set; }

}

public record GetWebhookResponseValue
{

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("createTime")]
    public required DateTimeOffset CreateTime { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public required DateTimeOffset LastUpdateTime { get; set; }

    [JsonPropertyName("deviceList")]
    public required string DeviceList { get; set; }

    [JsonPropertyName("enable")]
    public required bool Enable { get; set; }

}
