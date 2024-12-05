//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Repositories;

namespace Karamem0.SwitchBot.Services;

public interface ICreateWebhookService
{

    Task<CreateWebhookResponse> InvokeAsync(CreateWebhookRequest request);

}

public class CreateWebhookService(ISwitchBotRepository switchBotRepository) : ICreateWebhookService
{

    private readonly ISwitchBotRepository switchBotRepository = switchBotRepository;

    public async Task<CreateWebhookResponse> InvokeAsync(CreateWebhookRequest request)
    {
        await this
            .switchBotRepository.CreateWebhookAsync(request.Url, request.DeviceList)
            .ConfigureAwait(false);
        return new CreateWebhookResponse();
    }

}
