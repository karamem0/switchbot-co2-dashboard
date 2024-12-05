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

public interface IGetEventDataService
{

    Task<GetEventDataResponse> InvokeAsync(GetEventDataRequest request);

}

public class GetEventDataService(IBlobsRepository blobsRepository, IMapper mapper) : IGetEventDataService
{

    private readonly IBlobsRepository blobsRepository = blobsRepository;

    private readonly IMapper mapper = mapper;

    public async Task<GetEventDataResponse> InvokeAsync(GetEventDataRequest _)
    {
        var devices = await this
            .blobsRepository.GetDeviceDataAsync()
            .OrderBy(_ => _.Order)
            .ToListAsync()
            .ConfigureAwait(false);
        return new GetEventDataResponse()
        {
            Value = await Task.WhenAll(
                devices
                    .Select(this.mapper.Map<GetEventDataResponseValue>)
                    .Select(async value => this.mapper.Map(
                            await this
                                .blobsRepository.GetEventDataAsync(value.DeviceMac)
                                .ConfigureAwait(false),
                            value
                        )
                    )
            )
        };
    }

    public class MapperConfiguration : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            _ = config.NewConfig<DeviceData, GetEventDataResponseValue>();
            _ = config
                .NewConfig<EventData, GetEventDataResponseValue>()
                .Map(d => d.LastUpdateTime, s => s.TimeOfSample);
        }

    }

}
