//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Clients;
using Karamem0.SwitchBot.Repositories.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace Karamem0.SwitchBot.Repositories;

public interface ISwitchBotRepository
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

public class SwitchBotRepository(SwitchBotClient switchBotClient) : ISwitchBotRepository
{

    private readonly SwitchBotClient switchBotClient = switchBotClient;

    public async Task CreateWebhookAsync(
        Uri url,
        string deviceList,
        CancellationToken cancellationToken = default
    )
    {
        _ = await this
            .switchBotClient.SendAsync(
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
            .switchBotClient.SendAsync(
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
            .switchBotClient.SendAsync(
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
            .switchBotClient.SendAsync(
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
