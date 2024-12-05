//
// Copyright (c) 2024-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/switchbot-co2-dashboard/blob/main/LICENSE
//

using Karamem0.SwitchBot.Models;
using Karamem0.SwitchBot.Repositories;
using Karamem0.SwitchBot.Repositories.Models;
using Mapster;
using MapsterMapper;

namespace Karamem0.SwitchBot.Services;

public interface IGetWebhookService
{

    Task<GetWebhookResponse> InvokeAsync(GetWebhookRequest request);

}

public class GetWebhookService(ISwitchBotRepository switchBotRepository, IMapper mapper) : IGetWebhookService
{

    private readonly ISwitchBotRepository switchBotRepository = switchBotRepository;

    private readonly IMapper mapper = mapper;

    public async Task<GetWebhookResponse> InvokeAsync(GetWebhookRequest _)
    {
        var webhookUrls = await this
            .switchBotRepository.GetWebhookUrlsAsync()
            .ConfigureAwait(false);
        var webookDetails = await this
            .switchBotRepository.GetWebhookDetailsAsync(webhookUrls)
            .ConfigureAwait(false);
        return new GetWebhookResponse()
        {
            Value = this.mapper.Map<GetWebhookResponseValue[]>(webookDetails)
        };
    }

    public class MapperConfiguration : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            _ = config
                .NewConfig<WebhookDetails, GetWebhookResponseValue>()
                .Map(d => d.CreateTime, s => DateTimeOffset.FromUnixTimeMilliseconds(s.CreateTime))
                .Map(d => d.LastUpdateTime, s => DateTimeOffset.FromUnixTimeMilliseconds(s.LastUpdateTime));
        }

    }

}
