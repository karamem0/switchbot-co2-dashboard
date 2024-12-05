//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Models;

public record CreateWebhookRequest
{

    [JsonPropertyName("url")]
    public required Uri Url { get; set; }

    [JsonPropertyName("deviceList")]
    public required string DeviceList { get; set; } = "ALL";

}
