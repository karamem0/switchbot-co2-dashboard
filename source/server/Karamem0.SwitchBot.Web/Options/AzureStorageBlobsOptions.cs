//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

namespace Karamem0.SwitchBot.Options;

public record AzureStorageBlobsOptions
{

    public required Uri Endpoint { get; set; }

    public required string DeviceInfoContainerName { get; set; }

    public required string EventDataContainerName { get; set; }

    public string? ClientId { get; set; }

}
