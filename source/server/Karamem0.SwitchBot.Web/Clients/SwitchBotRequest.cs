//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

namespace Karamem0.SwitchBot.Clients;

public record SwitchBotRequest()
{

    public IDictionary<string, object>? Body { get; set; } = new Dictionary<string, object>();

}
