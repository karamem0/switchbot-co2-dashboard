//
// Copyright (c) 2024-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot/blob/main/LICENSE
//

using Karamem0.SwitchBot.Clients;
using Karamem0.SwitchBot.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Karamem0.SwitchBot.Services;

public interface ISwitchBotService
{

    Task CreateWebhookAsync(
        Uri url,
        string deviceList,
        CancellationToken cancellationToken = default
    );

    Task DeleteWebhookAsync(Uri url, CancellationToken cancellationToken = default);

    Task<WebhookDetails[]> GetWebhookDetailsAsync(Uri[] urls, CancellationToken cancellationToken = default);

    Task<Uri[]> GetWebhookUrlsAsync(CancellationToken cancellationToken = default);

}

public class SwitchBotService(SwitchBotClient switchBotClient) : ISwitchBotService
{

    private readonly SwitchBotClient switchBotClient = switchBotClient;

    public async Task CreateWebhookAsync(
        Uri url,
        string deviceList,
        CancellationToken cancellationToken = default
    )
    {
        _ = await this
            .switchBotClient
            .SendAsync(
                HttpMethod.Post,
                "/v1.1/Webhook/setupWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "setupWebhook",
                        ["url"] = url,
                        ["deviceList"] = deviceList
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task DeleteWebhookAsync(Uri url, CancellationToken cancellationToken = default)
    {
        _ = await this
            .switchBotClient
            .SendAsync(
                HttpMethod.Post,
                "/v1.1/Webhook/deleteWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "deleteWebhook",
                        ["url"] = url
                    }
                },
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task<WebhookDetails[]> GetWebhookDetailsAsync(Uri[] urls, CancellationToken cancellationToken = default)
    {
        var response = await this
            .switchBotClient
            .SendAsync(
                HttpMethod.Post,
                "/v1.1/Webhook/queryWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "queryDetails",
                        ["urls"] = urls
                    }
                },
                cancellationToken
            )
            .ContinueWith(response => response.Result.Body)
            .ConfigureAwait(false);
        return response?.Deserialize<WebhookDetails[]>() ?? [];
    }

    public async Task<Uri[]> GetWebhookUrlsAsync(CancellationToken cancellationToken = default)
    {
        var response = await this
            .switchBotClient
            .SendAsync(
                HttpMethod.Post,
                "/v1.1/Webhook/queryWebhook",
                new SwitchBotRequest()
                {
                    Body = new Dictionary<string, object>()
                    {
                        ["action"] = "queryUrl"
                    }
                },
                cancellationToken
            )
            .ContinueWith(response => response.Result.Body?["urls"])
            .ConfigureAwait(false);
        return response?.Deserialize<Uri[]>() ?? [];
    }

}
