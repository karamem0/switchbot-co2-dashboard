//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Karamem0.SwitchBot.Clients;

public record SwitchBotResponse
{

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }

    [JsonPropertyName("body")]
    public JsonNode? Body { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

}
