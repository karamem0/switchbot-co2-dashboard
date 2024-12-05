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

public interface IExecuteWebhookService
{

    Task<ExecuteWebhookResponse> InvokeAsync(ExecuteWebhookRequest request);

}

public class ExecuteWebhookService(IBlobsRepository blobsRepository, IMapper mapper) : IExecuteWebhookService
{

    private readonly IBlobsRepository blobsRepository = blobsRepository;

    private readonly IMapper mapper = mapper;

    public async Task<ExecuteWebhookResponse> InvokeAsync(ExecuteWebhookRequest request)
    {
        await this
            .blobsRepository.CreateEventDataAsync(this.mapper.Map<EventData>(request.Context))
            .ConfigureAwait(false);
        await this
            .blobsRepository.CreateEventDataHistoryAsync(this.mapper.Map<EventData>(request.Context))
            .ConfigureAwait(false);
        return new ExecuteWebhookResponse();
    }

    public class MapperConfiguration : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            _ = config
                .NewConfig<ExecuteWebhookRequestContext, EventData>()
                .Map(d => d.TimeOfSample, s => DateTimeOffset.FromUnixTimeMilliseconds(s.TimeOfSample));
        }

    }

}
