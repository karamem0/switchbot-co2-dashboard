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

public interface IDeleteWebhookService
{

    Task<DeleteWebhookResponse> InvokeAsync(DeleteWebhookRequest request);

}

public class DeleteWebhookService(ISwitchBotRepository switchBotRepository) : IDeleteWebhookService
{

    private readonly ISwitchBotRepository switchBotRepository = switchBotRepository;

    public async Task<DeleteWebhookResponse> InvokeAsync(DeleteWebhookRequest request)
    {
        await this
            .switchBotRepository.DeleteWebhookAsync(request.Url)
            .ConfigureAwait(false);
        return new DeleteWebhookResponse();
    }

}
